using System;

namespace tinyweb.framework.tests
{
    public class InvokeHandler
    {
        Route route = new Route("invoke");

        public IHandlerResult Get(int param1, string param2)
        {
            return new StringResult(param1 + param2);
        }

        public IHandlerResult Post(int param1, double param2, bool param3)
        {
            return new StringResult(param1.ToString() + param2.ToString() + param3.ToString());
        }

        public IHandlerResult Put(SimpleModel model)
        {
            return new StringResult("Result: " + (model.Number1 + model.Number2));
        }

        public IHandlerResult Delete(ComplexModel model)
        {
            return new StringResult(String.Format("{0} {1}", model.Label, (model.Numbers.Number1 + model.Numbers.Number2) * model.Factor));
        }
    }

    public class SimpleModel
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }

    public class ComplexModel
    {
        public SimpleModel Numbers { get; set; }
        public int Factor { get; set; }
        public string Label { get; set; }
    }
}