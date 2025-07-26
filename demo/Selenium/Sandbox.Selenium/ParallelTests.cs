[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace Sandbox.Selenium
{
    public class ParallelTests : IClassFixture<DriverFixture>
    {
        // Access fixture
        private readonly DriverFixture fixture;

    }
}
