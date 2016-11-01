import socket

host = "localhost"
port = 1690

primarySocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

primarySocket.bind((host, port))

while 1:
print primarySocket.recv(30)
