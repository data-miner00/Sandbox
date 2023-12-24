namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Security.AccessControl;
    using System.Security.Principal;

    public static class Security
    {
        public static void Demo1()
        {
            var currentUserIdentity = WindowsIdentity.GetCurrent();
            Console.WriteLine($"Current User Identity Name: {currentUserIdentity.Name}");

            DirectoryInfo directoryInfo = null!;
            var dir = @"C:\testFolderPermission";

            if (!Directory.Exists(dir))
            {
                directoryInfo = Directory.CreateDirectory(dir);
            }

            // Demo 1
            // Remove create files in folder
            DenyWriteRule(dir, currentUserIdentity);
            TryWrite(dir + @"\Test.txt", currentUserIdentity);

            AllowWriteRule(dir, currentUserIdentity);
            TryWrite(dir + @"\Test.txt", currentUserIdentity);
        }

        public static void Demo2()
        {
            AppDomain myDomain = Thread.GetDomain();

            myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            var myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            Console.WriteLine("{0} belongs to: ", myPrincipal.Identity.Name.ToString());

            var wbirFields = Enum.GetValues(typeof(WindowsBuiltInRole));

            foreach (var roleName in wbirFields)
            {
                try
                {
                    Console.WriteLine("{0}? {1}.", roleName, myPrincipal.IsInRole((WindowsBuiltInRole)roleName));
                    Console.WriteLine("The RID for this role is: " + (int)roleName);
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}: Could not obtain role for this RID.", roleName);
                }
            }

            // Get role using string value of role
            Console.WriteLine("Administrators? {0}", myPrincipal.IsInRole("BUILTIN\\Administrators"));
            Console.WriteLine("Users? {0}", myPrincipal.IsInRole("BUILTIN\\Users"));

            // Get role using enum
            Console.WriteLine("Administrators? {0}", myPrincipal.IsInRole(WindowsBuiltInRole.Administrator));
            Console.WriteLine("Users? {0}", myPrincipal.IsInRole(WindowsBuiltInRole.User));

            // Ger role using WellKnownSidType
            var sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            Console.WriteLine("WellKnowSidType BuiltinAdministratorsSid {0}? {1]", sid.Value, myPrincipal.IsInRole(sid));
            var sid2 = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
            Console.WriteLine("WellKnowSidType BuiltinUsersSid {0}? {1]", sid2.Value, myPrincipal.IsInRole(sid2));
        }

        /// <summary>
        /// Restricts a user to write to a directory.
        /// </summary>
        /// <param name="dir">The directory to be restricted.</param>
        /// <param name="currentUserIdentity">The user identity.</param>
        private static void DenyWriteRule(string dir, WindowsIdentity currentUserIdentity)
        {
            FileSystemAccessRule denyRule = new(
                currentUserIdentity.Name,
                FileSystemRights.WriteData,
                AccessControlType.Deny);

            var directoryInfo = new DirectoryInfo(dir);
            var directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.AddAccessRule(denyRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private static void AllowWriteRule(string dir, WindowsIdentity currentUserIdentity)
        {
            FileSystemAccessRule denyRule = new(
                currentUserIdentity.Name,
                FileSystemRights.WriteData,
                AccessControlType.Deny);

            FileSystemAccessRule allowRule = new(
                currentUserIdentity.Name,
                FileSystemRights.WriteData,
                AccessControlType.Allow);

            DirectoryInfo directoryInfo = new DirectoryInfo(dir);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.RemoveAccessRule(denyRule);
            directorySecurity.AddAccessRule(allowRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private static void TryWrite(string dir, WindowsIdentity currentUserIdentity)
        {
            try
            {
                var text = "The quick brown fox jumps over the lazy dog";
                File.WriteAllText(dir, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{currentUserIdentity.Name} cannot create files");
            }
        }
    }
}
