import math
import random
import matplotlib.pyplot as plt
import numpy
import time

correctData = []
noiseArray = []
combined = []

num_samples  = 100

typicalSwallow = {
	"amp" : 126,		# the peak of a typical swallow was at around 37.5ish 
						# microvolts, therefore, the peak mut be some factor 
						# of that given the standard deviation
	"mean":	89,			# arbitrary
	"sd"  : 1.35		# arbitrary
}

effortfulSwallow = {
	"amp" : 396,		# the peak of a typical swallow was at around 37.5ish 
						# microvolts, therefore, the peak mut be some factor 
						# of that given the standard deviation
	"mean":	89,			# arbitrary
	"sd"  : 1.79		# arbitrary	
}

mendelsohn = [
	{
		"amp" : 700,
		"mean": 69-10,
		"sd"  : 4.5,
	},
	{
		"amp" : 800,
		"mean": 76.7-10,
		"sd"  : 7.8,	
	}
]

def bellCurve(x, a, mu, sd):
	coef = a / (sd * math.sqrt(2 * math.pi))
	p_denominator = 2 * sd * sd
	p_numerator = (x - mu) * (x - mu)

	rhs = - p_numerator/p_denominator
	rhs = math.exp(rhs)
	return coef * rhs

def sawtoothCurve():
	pass

def populateNoise():
	# populate the noise array
	for x in range(0,num_samples):
		noiseArray.append(random.uniform(1.875,7.5)) #look at video for small noise
		noiseArray.append(1)
		if x > 0:
			noiseArray[x] = noiseArray[x-1]*0.5 + noiseArray[x]*0.5


def populateTypicalSwallow():
	populateNoise()

	# populate the rest of the array
	for x in range(0,num_samples):
		correctData.append(bellCurve(x, typicalSwallow["amp"], typicalSwallow["mean"], typicalSwallow["sd"]))
		combined.append(noiseArray[x] + correctData[x])

def populateEffortfulSwallow():
	populateNoise()
	
	# populate the rest of the array
	for x in range(0,num_samples):
		correctData.append(bellCurve(x, effortfulSwallow["amp"], effortfulSwallow["mean"], effortfulSwallow["sd"]))
		combined.append(noiseArray[x] + correctData[x])


def populateMendelsohn():
	populateNoise()

	# populate the rest of the array
	for x in range(0,num_samples):
		value = 0
		value = value + bellCurve(x, mendelsohn[0]["amp"], mendelsohn[0]["mean"], mendelsohn[0]["sd"])
		value = value + bellCurve(x-6.9, mendelsohn[1]["amp"], mendelsohn[1]["mean"], mendelsohn[1]["sd"])
		correctData.append(value)
		combined.append(noiseArray[x] + correctData[x])

#
# print out all the data for it
#

if __name__ == '__main__':
	#populateTypicalSwallow()
	#populateEffortfulSwallow()
	populateMendelsohn()

	print "Pure Data   |   Noise Data   |   Result"
	for x in range(0,num_samples):
		print "#{}: {}   |   {}   |   {}".format(x, correctData[x], noiseArray[x], combined[x])

	# setting the plot to be "animated"
	plt.ion()

	data_buffer = [0] * 50
	ax1=plt.axes()

	# make plot
	line, = plt.plot(data_buffer)
	plt.ylim([0,100])

	for i in range(0, len(combined)):
		print "reupdating the axis"
		# reupdate the y axises
		ymin = float(min(data_buffer))-1
		ymax = float(max(data_buffer))+10
		plt.ylim([ymin, ymax])

		# add the new data point
		data_buffer.append(combined[i])
		del data_buffer[0]

		# set the data again
		line.set_xdata(range(0, 50))
		line.set_ydata(data_buffer)

		# draw the figure
		plt.draw()
		print "attempting to draw index: {} with new data point: {}".format(i, combined[i])
		time.sleep(.1)

'''

	fig = plt.figure()
	ax = fig.add_subplot(1,1,1)

	line, = ax.plot(0, combined[0], 'b-')

	for x in range(0, num_samples):
		line.set_ydata(combined[x])
		fig.canvas.draw()
'''
	#matplotlib stuff here
		#define the axis

	#for i in range(0, num_samples):
	#	plt.axis([i-20,i,0,100])
	#	subset = combined[i-20:i]
	#	plt.plot(range(i-20,i),subset,"b-")
	#	plt.show()
	#for x in range(0, num_samples):
	#	line.set_xdata(numpy.append(line.get_xdata(), x))
	#	line.set_ydata(numpy.append(line.get_ydata(), combined[x]))
	#	plt.show()
	#	plt.draw()
	#	print "printing {}".format(x)
	#	time.sleep(1)
#'''
