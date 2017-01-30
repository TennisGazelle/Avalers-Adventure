import math
import time
import random
import socket
import matplotlib.pyplot as plt

import sys
import select
import termios

#declare variables
host = '127.0.0.1'
port = 3000

global lastNoise
lastNoise = 0

primarySocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # opens socket

def bellCurve(x, a, mu, sd):
	coef = a / (sd * math.sqrt(2 * math.pi))
	p_denominator = 2 * sd * sd
	p_numerator = (x - mu) * (x - mu)

	rhs = - p_numerator/p_denominator
	rhs = math.exp(rhs)
	return coef * rhs

def getNoise():
	global lastNoise
	now = random.uniform(1.875, 7.5)
	now = (now * 0.5) + (lastNoise * 0.5)
	lastNoise = now
	return now 

class GraphManager:
	"""docstring for GraphManager"""
	def __init__(self, dBuffer):
		self.dataBuffer = dBuffer

		# set the plot as "animated"
		plt.ion()
		# grab the line for updating
		self.line, = plt.plot(self.dataBuffer)
		# set the y axis
		plt.ylim([0,100])

		self.swallows = {
			"typical" : {
				"amp"	: 126,
				"mean"	: 6,
				"sd"	: 1.35,
				"counter" : 100,
				"max"	: 100
			},
			"effortful" : {
				"amp"	: 396,
				"mean"	: 6,
				"sd"	: 1.79,
				"counter" : 100,
				"max"	: 100
			},
			"mendelsohn1": {
				"amp" : 600.0,
				"sd"  : 4.6,
				"mean": 16.7,
				"counter" : 100,
				"max"	: 100	
			},
			"mendelsohn2": {
				"amp" : 800,
				"sd"  : 7.0,
				"mean": 30.0,
				"counter" : 100,
				"max"	: 100	
			},
			"mendelsohn3": {
				"amp" : 650.0,
				"sd"  : 7.6,
				"mean": 46.0,
				"counter" : 100,
				"max"	: 100	
			}
		}

		self.pause = False

	def updateGraph(self, point):
		if not self.pause:
			# update the axis
			ymin = float(min(self.dataBuffer))-1
			ymax = float(max(self.dataBuffer))+10
			#plt.ylim([ymin, ymax])

			# add the new datapoint to the buffer
			self.dataBuffer.append(point)
			del self.dataBuffer[0]

			# reset the data
			self.line.set_xdata(range(0, len(self.dataBuffer)))
			self.line.set_ydata(self.dataBuffer)

			# draw
			plt.draw()

	def startTypicalSwallow(self):
		print "starting typical swallow"
		self.swallows['typical']['counter'] = 0

	def startEffortfulSwallow(self):
		print "starting effortful swallow"
		self.swallows['effortful']['counter'] = 0

	def startMendelsohnSwallow(self):
		print "starting mendelsohn swallow"
		for i in range(1,4):
			key = "mendelsohn{}".format(i)
			self.swallows[key]['counter'] = 0

	def getDataPoint(self):
		currentPoint = getNoise()

		# go through the ballistics
		for t in self.swallows.keys():
			currentPoint += bellCurve(self.swallows[t]['counter'], # the x axis
									self.swallows[t]['amp'],
									self.swallows[t]['mean'],
									self.swallows[t]['sd'])

			self.swallows[t]['counter'] += 1
			if self.swallows[t]['counter'] > self.swallows[t]['max']:
				self.swallows[t]['counter'] = self.swallows[t]['max']

		return currentPoint

	def pauseGraph(self):
		self.pause = True

	def resumeGraph(self):
		self.pause = False

class KeyPoller():
	"""docstring for KeyPoller
		This was totally stolen from:
		http://stackoverflow.com/questions/13207678/whats-the-simplest-way-of-detecting-keyboard-input-in-python-from-the-terminal"""
	def __enter__(self):
		# Save the terminal settings
		self.fd = sys.stdin.fileno()
		self.new_term = termios.tcgetattr(self.fd)
		self.old_term = termios.tcgetattr(self.fd)

		# New terminal setting unbuffered
		self.new_term[3] = (self.new_term[3] & ~termios.ICANON & ~termios.ECHO)
		termios.tcsetattr(self.fd, termios.TCSAFLUSH, self.new_term)
		return self

	def __exit__(self):
		termios.tcsetattr(self.fd, termios.TCSAFLUSH, self.old_term)

	def poll(self):
		dr, dw, de = select.select([sys.stdin], [], [], 0)
		if not dr == []:
			return sys.stdin.read(1)
		return None

def sendData(point):
	packetData = str(point).encode('ascii') #encode packet to bytes
	primarySocket.sendto(packetData, (host, port)) #send packet
	#print 
	#print packetData, "sent"

def main():
	buffer_size = 100

	graph = GraphManager([0] * buffer_size)
	counter = 0
	kp = KeyPoller()
	while 1:
		# get the data point, send it and draw it
		swallowData = graph.getDataPoint()
		sendData(swallowData)
		graph.updateGraph(swallowData)

		counter += 1

		# check for keyboard input
		c = kp.poll()
		if c is not None:
			# if it is, pick the approrpiate swallow and initialize counter
			print 'keyboard detected as {}'.format(c)
			if c == 't' or c == 'T':
				graph.startTypicalSwallow()
			elif c == 'e' or c == 'E':
				graph.startEffortfulSwallow()
			elif c == 'm' or c == 'M':
				graph.startMendelsohnSwallow()
			elif c == 'p' or c == 'P':
				graph.pauseGraph()
			elif c == 'r' or c == 'R':
				graph.resumeGraph()
			elif c == 'q' or c == 'Q':
				break

		#print counter, swallowData
		# wait
		#time.sleep(.1)

if __name__ == '__main__':
	main()
