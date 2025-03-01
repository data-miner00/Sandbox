namespace Sandbox.Silo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    internal class FormatterSerialization
    {
        /// <summary>
        /// This can be used to create an object that dont have public constructor.
        /// </summary>
        public static void GetForbiddenObject()
        {
            var exception = FormatterServices.GetUninitializedObject(typeof(Exception)) as Exception;
        }
    }
}
