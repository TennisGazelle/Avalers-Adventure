import serial
from sys import platform

class SerialManager:
	""" Manager class for serial communications that will be used by Input manager in the near future"""
	
	def __init__(self):
		# define the default baud
		self.baud = 9600
		self.ser = None
		self.buffer = [0,1]

		self.attemptConnection()

	def attemptConnection(self):
		if self.ser is not None:
			print ("connection already made")
		else:
			try:
				if platform == "linux" or platform == "linux2":
					self.ser = serial.Serial('/dev/ttyACM0', self.baud)
				elif platform == "win32":
					self.ser = serial.Serial('COM3', self.baud)
			except serial.SerialException as se:
				print ("no connection made:", se)

	def getNextValue(self):
		if self.ser is None:
			return None
		self.buffer[0] = str( self.ser.readline() )
		return self.buffer[0].rstrip("\n")


if __name__ == '__main__':
	s = SerialManager()
	while True:
		print (s.getNextValue().lstrip("b'").rstrip("\\r\\n'"))

	




#s = [0,1]

#while True:
#	s[0] = str( ser.readline() )
#	print "str:", s[0]
