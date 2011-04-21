namespace tinyweb.framework
{
    public interface IFilterFactory
    {
        object Create(FilterData filterData);
    }
}