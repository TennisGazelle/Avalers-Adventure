import socket

host = '127.0.0.1'
port = 3000
BUFFER_SIZE = 1024

primarySocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

primarySocket.bind((host, port))

while(True):
    data, addr = primarySocket.recvfrom(BUFFER_SIZE)
    text = data.decode('ascii')
    print ('received: {} from {}'.format(text, addr))
