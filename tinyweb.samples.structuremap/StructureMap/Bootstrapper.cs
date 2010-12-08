using StructureMap;

namespace tinyweb.samples.structuremap
{
    public static class Bootstrapper
    {
        public static void ConfigureStructureMap()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new ServiceRegistry()));
        }
    }
}