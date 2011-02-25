using StructureMap.Configuration.DSL;

namespace tinyweb.framework.tests
{
    public class BarRegistry : Registry
    {
        public BarRegistry()
        {
            For<IBarRepository>().Add<BarRepository>();
        }
    }
}
