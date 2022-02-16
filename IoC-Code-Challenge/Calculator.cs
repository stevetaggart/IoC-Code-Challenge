namespace IoC_Code_Challenge
{
    public class Calculator : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Subtract(int x, in int y)
        {
            return x - y;
        }
    }

    public interface ICalculator
    {
        public int Add(int x, int y);
        public int Subtract(int x, in int y);
    }

    public class CalculatorController
    {
        private readonly ICalculator calculator;

        public CalculatorController(ICalculator calculator)
        {
            this.calculator = calculator;
        }

        public int PerformCalculations(int x, int y) => calculator.Add(x, y);
    }
}
