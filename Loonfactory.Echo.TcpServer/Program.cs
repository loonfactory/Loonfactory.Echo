using System.Net.Sockets;
using System.Net;
using System.Text;

const int Port = 29290;

var listener = new TcpListener(IPAddress.Any, Port);
listener.Start();
Console.WriteLine($"TCP Echo Server is running on port {Port}");

while (true)
{
    var client = await listener.AcceptTcpClientAsync();
    _ = Task.Run(() => HandleClient(client));
}

async Task HandleClient(TcpClient client)
{
    using var stream = client.GetStream();
    var endPoint = (client.Client.RemoteEndPoint as IPEndPoint)!;
    Console.WriteLine($"[{endPoint.Address}:{endPoint.Port}] Client connected.");
    var buffer = new byte[1024];
    int count;

    try
    {
        while ((count = await stream.ReadAsync(buffer)) != 0)
        {
            Console.WriteLine($"[{endPoint.Address}:{endPoint.Port}] {BitConverter.ToString(buffer, 0, count)}");
            await stream.WriteAsync(buffer.AsMemory(0, count));
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[{endPoint.Address}:{endPoint.Port}] Client handling error: " + ex.Message);
    }
    finally
    {
        client.Close();
        Console.WriteLine($"[{endPoint.Address}:{endPoint.Port}] Client disconnected.");
    }
}
