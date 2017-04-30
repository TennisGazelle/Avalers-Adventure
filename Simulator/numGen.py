import math
import time
import random
import socket
import matplotlib.pyplot as plt

import sys
import select
import termios
import KeyPoller

import SerialManager

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
			plt.ylim([ymin, ymax])

			# add the new datapoint to the buffer
			self.dataBuffer.append(point)
			del self.dataBuffer[0]

			# reset the data
			self.line.set_xdata(range(0, len(self.dataBuffer)))
			self.line.set_ydata(self.dataBuffer)

			# draw
			plt.draw()

	def startTypicalSwallow(self):
		print ("starting typical swallow")
		self.swallows['typical']['counter'] = 0

	def startEffortfulSwallow(self):
		print ("starting effortful swallow")
		self.swallows['effortful']['counter'] = 0

	def startMendelsohnSwallow(self):
		print ("starting mendelsohn swallow")
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

def sendData(point):
	packetData = str(point).encode('ascii') #encode packet to bytes
	primarySocket.sendto(packetData, (host, port)) #send packet
	#print 
	#print packetData, "sent"

def main():
	buffer_size = 100

	graph = GraphManager([0] * buffer_size)
	counter = 0
	kp = KeyPoller.KeyPoller()
	se = SerialManager.SerialManager()

	while 1:
		# get the data point, send it and draw it
		swallowData = se.getNextValue()
		if swallowData is None:
			swallowData = graph.getDataPoint()

		sendData(swallowData)
		graph.updateGraph(swallowData)

		counter += 1

		# check for keyboard input
		c = kp.poll()
		if c is not None:
			# if it is, pick the approrpiate swallow and initialize counter
			print ('keyboard detected as {}'.format(c))
			if c == 't' or c == 'T' or counter % 50 == 0:
				graph.startTypicalSwallow()
			elif c == 'e' or c == 'E':
				graph.startEffortfulSwallow()
			elif c == 'm' or c == 'M' or (counter % 100) == 0:
				graph.startMendelsohnSwallow()
			elif c == 'p' or c == 'P':
				graph.pauseGraph()
			elif c == 'r' or c == 'R':
				graph.resumeGraph()
			elif c == 'a' or c == 'A':
				se.attemptConnection()
			elif c == 'q' or c == 'Q':
				break

		#print counter, swallowData
		# wait
		#time.sleep(.1)

# def emergencyMain():
# 	ser = SerialManager.SerialManager()
# 	while True:
# 		value = ser.getNextValue().lstrip("b'").rstrip("\\r\\n'")
# 		sendData(value)


if __name__ == '__main__':
	main()
