using System.Net.Sockets;
using System.Net;
using System.Text;

UdpClient udpClient = new();
Console.Write("Please enter the UDP server address and port (ex: 192.168.0.1:1234): ");
var input = Console.ReadLine();

IPEndPoint? serverEndPoint;
while (!IPEndPoint.TryParse(input!, out serverEndPoint))
{
    Console.WriteLine("Invalid input. Please try again.");
    input = Console.ReadLine();
}

udpClient.Connect(serverEndPoint);

while (true)
{
    input = Console.ReadLine();
    var buffer = Encoding.UTF8.GetBytes(input!);
    udpClient.Send(buffer, buffer.Length);

    Console.WriteLine("Send: " + input);

    buffer = udpClient.Receive(ref serverEndPoint);

    Console.WriteLine("Received: " + BitConverter.ToString(buffer));
}