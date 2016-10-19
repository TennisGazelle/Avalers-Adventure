import math
import random
import matplotlib.pyplot as plt
import numpy

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
		"mean": 69,
		"sd"  : 4.5,
	},
	{
		"amp" : 800,
		"mean": 76.7,
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
		#noiseArray.append(random.uniform(1.875,7.5)) #look at video for small noise
		noiseArray.append(1)
		if x > 0:
			noiseArray[x] = noiseArray[x-1]*0.2 + noiseArray[x]*0.8	


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
	populateEffortfulSwallow()
	#populateMendelsohn()

	print "Pure Data   |   Noise Data   |   Result"
	for x in range(0,num_samples):
		print "{}   |   {}   |   {}".format(correctData[x], noiseArray[x], combined[x])

	fig = plt.figure()
	ax = fig.add_subplot(1,1,1)

	line, = ax.plot(0, combined[0], 'b-')

	for x in range(0, num_samples):
		line.set_ydata(combined[x])
		fig.canvas.draw()



'''
	# matplotlib stuff here
		#define the axis
	plt.axis([0,num_samples,0,100])

	#plt.show()
	for x in range(0, num_samples):
		line.set_xdata(numpy.append(line.get_xdata(), x))
		line.set_ydata(numpy.append(line.get_ydata(), combined[x]))
		plt.draw()


	#plt.plot(range(0,num_samples),combined,"b-")
'''