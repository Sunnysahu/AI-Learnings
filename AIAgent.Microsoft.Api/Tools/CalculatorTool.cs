namespace AIAgent.Microsoft.Api.Tools;

public static class CalculatorTool
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Subtract(int a, int b)
    {
        return a - b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return (double) a / b;
    }
}