namespace Sandbox.ConsoleApp
{
    using System;
    using Sandbox.Concepts.Bcl.Threading;
    using Sandbox.Nuget.NetCore;

    /// <summary>
    /// A sandbox class to play around and experiment with.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point of the sandbox.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            //SendingEmail.SendEmail("ben@contoso.com", "noreply@contoso.com");
            Monitors.Example();
            Console.WriteLine(Sample.Greetings);
            var bo = Role.Administrator < Role.Moderator;
        }
    }

    enum Role
    {
        Administrator,
        Moderator,
        User,
        Guest,
    }
}
