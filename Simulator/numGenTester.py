import unittest
from mock import MagicMock
import numGen

class TestNumGen(unittest.TestCase):
	# def setUp(self):
	# 	self.graph = GraphManager([0] * 10)

	def test_bellcurve(self):
		val = numGen.bellCurve(0, 0, 0, 1)
		self.assertEqual(val, 0)

	def test_sendData(self):



if __name__ == '__main__':
	unittest.main()
