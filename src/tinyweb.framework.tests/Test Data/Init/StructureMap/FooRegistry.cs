using StructureMap.Configuration.DSL;

namespace tinyweb.framework.tests
{
    public class FooRegistry : Registry
    {
        protected override void configure()
        {
            ForRequestedType<IFooRepository>().AddConcreteType<FooRepository>();

            base.configure();
        }
    }
}
