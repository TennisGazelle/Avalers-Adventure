import math
import random
import matplotlib.pyplot as plt

typicalSwallow = []
noiseArray = []
combined = []

highAmplitude = 126     #the peak of a typical swallow was at around 37.5ish 
                        #microvolts, therefore, the peak mut be some factor 
                        #of that given the standard deviation

mean = 89               #arbitrary

standard_deviation = 1.35  #also arbitrary, makes thing easier


def bellCurve(x, a, mu, sd):
	coef = a / (sd * math.sqrt(2 * math.pi))
	p_denominator = 2 * sd * sd
	p_numerator = (x - mu) * (x - mu)

	rhs = - p_numerator/p_denominator
	rhs = math.exp(rhs)
	return coef * rhs

for x in range(0,100):
	noiseArray.append(random.uniform(1.875,7.5)) #look at video for small noise

for x in range(0,100):
	if x > 0:
		noiseArray[x] = noiseArray[x-1]*0.2 + noiseArray[x]*0.8
	typicalSwallow.append(bellCurve(x, highAmplitude, mean, standard_deviation))
	combined.append(noiseArray[x] + typicalSwallow[x])

print "Pure Data   |   Noise Data   |   Result"
for x in range(0,100):
	print "{}   |   {}   |   {}".format(typicalSwallow[x], noiseArray[x], combined[x])

plt.plot(range(0,100),combined,"b-")

plt.axis([0,100,0,50])

plt.show()
