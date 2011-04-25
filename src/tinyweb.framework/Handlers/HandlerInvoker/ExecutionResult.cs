namespace tinyweb.framework
{
    public class ExecutionResult
    {
        public IResult BeforeResult { get; set; }
        public IResult Result { get; set; }
        public IResult AfterResult { get; set; }
    }
}