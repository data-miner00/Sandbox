namespace Sandbox.Concepts
{
    public class Modifiers
    {
        private protected string privateProtectedString = string.Empty;
        protected internal string protectedInternalString = string.Empty;
        public static string StaticVariable = string.Empty;
        public string classVariable = string.Empty;

        public Modifiers()
        {
            StaticVariable = "ModifiedByInstance";
            this.classVariable = "ModifiedByInstance";
        }

        static Modifiers()
        {
            StaticVariable = "ModifiedByStaticConstructor";
        }
    }
}
