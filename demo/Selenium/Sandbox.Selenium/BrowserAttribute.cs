namespace Sandbox.Selenium
{
    using System.Reflection;
    using Xunit.Sdk;

    internal class BrowserAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            base.Before(methodUnderTest);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
        }
    }
}
