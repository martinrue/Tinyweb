using StructureMap.Configuration.DSL;

namespace tinyweb.framework.tests
{
    public class BarRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IBarRepository>().AddConcreteType<BarRepository>();

            base.configure();
        }
    }
}
