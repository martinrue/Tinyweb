using StructureMap.Configuration.DSL;

namespace tinyweb.framework.tests
{
    public class FooRegistry : Registry
    {
        public FooRegistry()
        {
            For<IFooRepository>().Add<FooRepository>();
        }
    }
}
