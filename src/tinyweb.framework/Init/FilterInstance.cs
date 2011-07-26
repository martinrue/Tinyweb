namespace tinyweb.framework
{
    public class FilterInstance
    {
        public object Instance { get; set; }
        public bool BeforeFilter { get; set; }
        public bool AfterFilter { get; set; }
    }
}