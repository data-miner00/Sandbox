namespace Sandbox.Concepts.Bcl
{
    using System;
    using System.Reflection;

    public static class ReflectionExamples
    {
        public static void GettingType()
        {
            // 1.
            var obj = new object();
            var type = obj.GetType();

            // 2.
            var type2 = Type.GetType("System.Object", false, true);

            // 3.
            var type3 = typeof(object);
        }

        public static void TypeProperties(Type type)
        {
            Console.WriteLine(type.Name);
            Console.WriteLine(type.FullName);
            Console.WriteLine(type.Namespace);
            Console.WriteLine(type.BaseType);

            var underlyingSystemType = type.UnderlyingSystemType;

            if (underlyingSystemType is not null)
            {
                Console.WriteLine(underlyingSystemType.Name);
                Console.WriteLine(underlyingSystemType.Assembly);
            }
        }

        public static void BooleanProperties(Type type)
        {
            Console.WriteLine(type.IsAbstract);
            Console.WriteLine(type.IsArray);
            Console.WriteLine(type.IsClass);
            Console.WriteLine(type.IsCOMObject);
        }

        public static void GettingPublicMembers(Type type)
        {
            var members = type.GetMembers();

            foreach (var member in members)
            {
                Console.WriteLine(member.DeclaringType);
                Console.WriteLine(member.MemberType);
                Console.WriteLine(member.Name);
            }
        }

        public static void ReflectingInfos(Type type)
        {
            foreach (var method in type.GetMethods())
            {
                Console.WriteLine(method.Name);
            }

            var toStringMethodInfo = type.GetMethod("ToString");
            Console.WriteLine(toStringMethodInfo?.Name);

            foreach (var field in type.GetFields())
            {
                Console.WriteLine(field.Name);
            }

            foreach (var property in type.GetProperties())
            {
                Console.WriteLine(property.Name);
            }
        }

        public static void AssemblyReflection(string dllPath)
        {
            Assembly.Load(Assembly.GetExecutingAssembly().Location);

            // Load a dll
            var assembly = Assembly.LoadFile(dllPath);

            // Get the class type info
            var vehicleType = assembly.GetType("Vehicle");

            // Create an instance of Vehicle
            var vehicle = Activator.CreateInstance(vehicleType);

            // Call a method with parameters
            object[] parameters = [30, 80, 120, 60];

            var methodInfo = vehicleType.GetMethod("MethodWith4InputParameters");
            var result = (int)methodInfo.Invoke(vehicle, parameters);
        }
    }
}
