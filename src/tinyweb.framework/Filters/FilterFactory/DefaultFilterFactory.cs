using StructureMap;

namespace tinyweb.framework
{
    public class DefaultFilterFactory : IFilterFactory
    {
        public object Create(FilterData filterData)
        {
            return ObjectFactory.GetInstance(filterData.Type);
        }
    }
}