namespace Rms.Application.Tests
{
    public class UnitTest1
    {
        [Fact]
        [Trait("Scope", "Unit")]
        public void Application_UnitTest_Example()
        {
            var result = 3 + 4;
            Assert.Equal(7, result);
        }
        [Fact]
        [Trait("Scope", "Integration")]
        public void Application_Integration_Example()
        {
            var result = 5 + 4;
            Assert.Equal(9, result);
        }
    }
}