using System.Net.Sockets;
using System.Net;
using System.Text;

Console.Write("Please enter the Tcp echo server address and port (ex: 192.168.0.1:1234): ");
TcpClient tcpClient = new();
var input = Console.ReadLine();

IPEndPoint? serverEndPoint;
while (!IPEndPoint.TryParse(input!, out serverEndPoint))
{
    Console.WriteLine("Invalid input. Please try again.");
    input = Console.ReadLine();
}

tcpClient.Connect(serverEndPoint);
using var stream = tcpClient.GetStream();

while (true)
{
    Console.Write("Send: ");
    input = Console.ReadLine();
    var buffer = Encoding.UTF8.GetBytes(input!);

    await stream.WriteAsync(buffer);

    var count = await stream.ReadAsync(buffer.AsMemory());

    Console.WriteLine("Received: " + Encoding.UTF8.GetString(buffer));
}