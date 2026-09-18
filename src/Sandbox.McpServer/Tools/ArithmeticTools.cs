namespace Sandbox.McpServer.Tools
{
    using ModelContextProtocol.Server;
    using System.ComponentModel;

    internal class ArithmeticTools
    {
        [McpServerTool]
        [Description("Adds two numbers together.")]
        public int AddNumber(
            [Description("First number to add.")] int numA,
            [Description("Second number to add.")] int numB)
        {
            return numA + numB;
        }

        [McpServerTool]
        [Description("Deduct second number from the first number.")]
        public int DeductNumber(
            [Description("First number.")] int numA,
            [Description("Second number to deduct from.")] int numB)
        {
            return numA - numB;
        }

        [McpServerTool]
        [Description("Multiplify two numbers together.")]
        public int MultiplyNumber(
            [Description("First number to multiply.")] int numA,
            [Description("Second number to multiply.")] int numB)
        {
            return numA * numB;
        }

        [McpServerTool]
        [Description("Divide two numbers together.")]
        public int DivideNumber(
            [Description("First number")] int numA,
            [Description("Second number to divide.")] int numB)
        {
            return numA / numB;
        }
    }
}
