import random
import socket
import time

generatorNdx = 0
host = "localhost"
port = 1690

primarySocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

while generatorNdx<100:
    if (generatorNdx%5) == 0:
        print(random.randint(25,75))
        primarySocket.sendto(str(10), (host, port))
    else:
        print(random.randint(1,5))
        primarySocket.sendto(str(10), (host, port))
    generatorNdx+=1
    time.sleep(1)

