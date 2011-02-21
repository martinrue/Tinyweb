namespace tinyweb.samples.structuremap.Services
{
    public class HelloService : IHelloService
    {
        public string SayHello()
        {
            return "Hello World";
        }
    }
}