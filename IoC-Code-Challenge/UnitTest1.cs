using Xunit;

namespace IoC_Code_Challenge
{
    public class UnitTest1
    {
        [Fact]
        public void Container_can_register_types()
        {
            var container = new IocContainer();
        }
    }
}