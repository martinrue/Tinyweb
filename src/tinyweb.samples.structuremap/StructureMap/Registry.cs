using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace tinyweb.samples.structuremap
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.With<DefaultConventionScanner>();
            });
        }
    }
}