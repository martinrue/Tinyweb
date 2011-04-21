namespace tinyweb.framework
{
    public class ExecutionResult
    {
        public IHandlerResult BeforeResult { get; set; }
        public IHandlerResult Result { get; set; }
        public IHandlerResult AfterResult { get; set; }
    }
}