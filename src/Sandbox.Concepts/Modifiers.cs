namespace Sandbox.Concepts
{
    public class Modifiers
    {
        #region Fields
        public static string StaticVariable = "AssignedStatically";

#pragma warning disable SA1401 // Fields should be private
#pragma warning disable S1104 // Fields should not have public accessibility
        public string ClassVariable;
#pragma warning restore S1104 // Fields should not have public accessibility
        protected internal string ProtectedInternalString;
        private protected string privateProtectedString;
#pragma warning restore SA1401 // Fields should be private
        #endregion

        #region Constructors
#pragma warning disable S3963 // "static" fields should be initialized inline
        static Modifiers()
#pragma warning restore S3963 // "static" fields should be initialized inline
        {
            StaticVariable = "ModifiedByStaticConstructor";
        }

        public Modifiers()
        {
            this.privateProtectedString = "privateProtected";
            this.ProtectedInternalString = "ProtectedInternal";
#pragma warning disable S3010 // Static fields should not be updated in constructors
            StaticVariable = "ModifiedByInstance";
#pragma warning restore S3010 // Static fields should not be updated in constructors
            this.ClassVariable = "ModifiedByInstance";
        }
        #endregion

        #region Methods
        public ref string GetRefLocalToStr()
        {
            return ref this.ClassVariable;
        }
        #endregion
    }
}
