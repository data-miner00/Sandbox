namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public static class SocketsExample
    {
        public static void Server()
        {
            var @lock = new object();
            var listClients = new Dictionary<int, TcpClient>();

            var count = 1;
            var port = 5000;

            var serverSocket = new TcpListener(IPAddress.Any, port);
            serverSocket.Start();

            Console.WriteLine("Server listening on port " + port);

            while (true)
            {
                var client = serverSocket.AcceptTcpClient();
                lock (@lock)
                {
                    listClients.Add(count++, client);
                }

                Console.WriteLine("Someone connected!");

                var thread = new Thread((object o) =>
                {
                    var id = (int)o;
                    TcpClient client;

                    lock (@lock)
                    {
                        client = listClients[id];
                    }

                    while (true)
                    {
                        NetworkStream stream = client.GetStream();
                        var buffer = new byte[1024];
                        var byteCount = stream.Read(buffer, 0, buffer.Length);

                        if (byteCount == 0)
                        {
                            break;
                        }

                        var data = Encoding.ASCII.GetString(buffer, 0, byteCount);
                        Broadcast(data);
                        Console.WriteLine(data);
                    }

                    lock (@lock)
                    {
                        listClients.Remove(id);
                    }

                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Close();
                });

                thread.Start();
            }

            void Broadcast(string data)
            {
                var buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

                lock (@lock)
                {
                    foreach (var client in listClients.Values)
                    {
                        var stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }

        public static void Client()
        {
            var address = "localhost";
            var port = 5000;

            var ip = IPAddress.Parse(address);
            var client = new TcpClient();
            client.Connect(ip, port);
            Console.WriteLine("Client connected!");
            var stream = client.GetStream();
            var thread = new Thread((object o) =>
            {
                var tcpClient = (TcpClient)o;
                var stream = tcpClient.GetStream();
                var receivedBytes = new byte[1024];
                int byteCount = 0;

                while ((byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Console.Write(Encoding.ASCII.GetString(receivedBytes, 0, byteCount));
                }
            });

            thread.Start(client);

            string str;
            while (!string.IsNullOrEmpty(str = Console.ReadLine()))
            {
                var buffer = Encoding.ASCII.GetBytes(str);
                stream.Write(buffer, 0, buffer.Length);
            }

            client.Client.Shutdown(SocketShutdown.Send);
            thread.Join();
            stream.Close();
            client.Close();
            Console.WriteLine("Disconnect from server");
        }
    }
}
