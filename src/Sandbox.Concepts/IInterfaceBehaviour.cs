namespace Sandbox.Concepts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface is interesting as it allow private record, static method in its definition.
    /// </summary>
    internal interface IInterfaceBehaviour
    {
        private record iRecord;

        private static void Display(int i)
        {
            Console.WriteLine(i);
        }

        public static string ReturnEmptyString() => string.Empty;

        public abstract void DisplayVersion2();
    }
}
