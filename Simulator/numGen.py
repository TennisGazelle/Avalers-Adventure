import random
import socket
import time

host = '127.0.0.1'
port = 3000

primarySocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # opens socket

timeLapse = 0; #so there isnt a swallow every single second
while 1:
    if (timeLapse%3) == 0: #checks for every third swallow
        swallowData = random.randint(25,75)
    else:
        swallowData = random.randint(1,5)
        
    packetData = str(swallowData).encode('ascii') #encode packet to bytes
    primarySocket.sendto(packetData, (host, port)) #send packet
    
    timeLapse+=1
    time.sleep(1)

