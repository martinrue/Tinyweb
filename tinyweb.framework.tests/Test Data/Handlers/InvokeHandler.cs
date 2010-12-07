using System;

namespace tinyweb.framework.tests
{
    public class InvokeHandler
    {
        Route route = new Route("invoke");

        public StringResult Get(int param1, string param2)
        {
            return param1 + param2;
        }

        public StringResult Post(int param1, double param2, bool param3)
        {
            return param1.ToString() + param2.ToString() + param3.ToString();
        }

        public StringResult Put(SimpleModel model)
        {
            return "Result: " + (model.Number1 + model.Number2);
        }

        public StringResult Delete(ComplexModel model)
        {
            return String.Format("{0} {1}", model.Label, (model.Numbers.Number1 + model.Numbers.Number2) * model.Factor);
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