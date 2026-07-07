using System.ComponentModel;

namespace AIAgent.Microsoft.Api.Tools;

public static class CalculatorTools
{
    [Description("Adds two numbers together.")]
    public static int Add
    (
        [Description("First number")] int firstNumber,
        [Description("Second number")] int secondNumber
    )
    {
        return firstNumber + secondNumber;
    }

    [Description("Multiply two numbers.")]
    public static int Multiply
    (
        [Description("First number")] int firstNumber, 
        [Description("Second number")] int secondNumber
    )
    {
        return firstNumber * secondNumber;
    }
}