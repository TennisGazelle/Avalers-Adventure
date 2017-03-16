import RPi.GPIO as GPIO

# for some basic stuff, go here, http://makezine.com/projects/tutorial-raspberry-pi-gpio-pins-and-python/
# for a very specific script that helps, go here, https://acaird.github.io/computers/2015/01/07/raspberry-pi-fsr

GPIO.setmode(GPIO.BCM)
GPIO.setup(23, GPIO.IN, pull_up_down = GPIO.PUD_DOWN)
GPIO.setup(24, GPIO.IN, pull_up_down = GPIO.PUD_UP)

while True:
	if(GPIO.input(23) ==1):
		print(“Button 1 pressed”)
	if(GPIO.input(24) == 0):
		print(“Button 2 pressed”)

GPIO.cleanup()
