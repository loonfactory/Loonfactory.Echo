using System.Net.Sockets;
using System.Net;

const int Port = 29291;

UdpClient udpServer = new(Port);
Console.WriteLine("UDP Echo Server is running on port " + Port);

try
{
    while (true)
    {
        // 클라이언트의 IP 엔드포인트를 저장할 변수
        var clientEP = new IPEndPoint(IPAddress.Any, 0);

        // 클라이언트로부터 메시지를 받음
        byte[] receivedBytes = udpServer.Receive(ref clientEP);

        // 받은 메시지를 클라이언트에게 다시 보냄
        udpServer.Send(receivedBytes, receivedBytes.Length, clientEP);
        Console.WriteLine("Received: " + BitConverter.ToString(receivedBytes) + " from " + clientEP.ToString());
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    udpServer.Close();
}