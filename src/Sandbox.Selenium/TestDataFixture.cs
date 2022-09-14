namespace Sandbox.Selenium
{
    using AutoFixture;

    internal class TestDataFixture
    {
        public IEnumerable<string> ItemsToAdd { get; set; }

        public IEnumerable<string> ItemsToCheck { get; set; }

        public TestDataFixture()
        {
            var autoFixture = new Fixture();
            this.ItemsToAdd = autoFixture.CreateMany<string>(5).ToList();
            this.ItemsToCheck = this.ItemsToAdd.Skip(3).ToList();
        }
    }
}
