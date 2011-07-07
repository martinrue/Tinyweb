using System;

namespace tinyweb.framework.Helpers
{
    public class FakeApplicationPathProvider : IApplicationPathProvider
    {
        public string Path { get; set; }

        public FakeApplicationPathProvider()
        {
            Path = String.Empty;
        }

        public string GetApplicationPath()
        {
            return Path;
        }
    }
}