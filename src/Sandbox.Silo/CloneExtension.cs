namespace Sandbox.Silo
{
    internal static class CloneExtension
    {
        public static T Clone<T>(this T obj)
            where T : class, new()
        {
            var newObj = new T();
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj, null);
                property.SetValue(newObj, value);
            }

            return newObj;
        }
    }
}
