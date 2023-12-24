namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;

    public static class Networking
    {
        public static void Example()
        {
            var hostName = Dns.GetHostName();
            Console.WriteLine("Hostname: " + hostName);

            // Use another host
            IPHostEntry host = Dns.GetHostEntry("wikipedia.org");

            var addresses = host.AddressList;

            foreach (var address in addresses)
            {
                Console.WriteLine(address);
            }
        }

        public static void Example2()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpStatistics tcpstat = properties.GetTcpIPv4Statistics();

            Console.WriteLine("Minimum Transmission Timeout: " + tcpstat.MinimumTransmissionTimeout);
            Console.WriteLine("Maximum Transmission Timeout: " + tcpstat.MaximumTransmissionTimeout);
            Console.WriteLine("Current Connection: " + tcpstat.CurrentConnections);
            Console.WriteLine("Cumulative Connection: " + tcpstat.CumulativeConnections);
            Console.WriteLine("Connections Initiated: " + tcpstat.ConnectionsInitiated);
            Console.WriteLine("Connections Accepted: " + tcpstat.ConnectionsAccepted);
            Console.WriteLine("Failed Connection Attempts: " + tcpstat.FailedConnectionAttempts);
            Console.WriteLine("Reset Connections: " + tcpstat.ResetConnections);

            Console.WriteLine("Segments Received: " + tcpstat.SegmentsReceived);
            Console.WriteLine("Segments Sent: " + tcpstat.SegmentsSent);
            Console.WriteLine("Segments Resent: " + tcpstat.SegmentsResent);
        }

        public static void UsingPing()
        {
            using var ping = new Ping();

            var reply = ping.Send("jaring.my", 200);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine(reply.RoundtripTime);
            }
            else
            {
                Console.WriteLine(reply.Status);
            }
        }
    }
}
