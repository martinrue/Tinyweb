using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace tinyweb.samples.structuremap
{
    public class ServiceRegistry : Registry
    {
        protected override void configure()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.With<DefaultConventionScanner>();
            });

            base.configure();
        }
    }
}