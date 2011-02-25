using StructureMap.Configuration.DSL;

namespace tinyweb.samples.structuremap
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
        }
    }
}