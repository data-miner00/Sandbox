using Fleck;

namespace Server
{
    internal class Program
    {
        private const string WebSocketUrl = "ws://0.0.0.0:8181";

        static void Main(string[] args)
        {
            using var server = new WebSocketServer(WebSocketUrl);

            List<IWebSocketConnection> connections = [];

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("New connection opened.");
                    connections.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("Connection closed.");
                    connections.Remove(socket);
                };

                socket.OnMessage = message => 
                {
                    Console.WriteLine($"Received message: {message}");
                    socket.Send($"Echo: {message}");

                    foreach (var conn in connections)
                    {
                        if (conn != socket)
                        {
                            conn.Send($"Broadcast: {message}");
                        }
                    }
                };
            });

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
