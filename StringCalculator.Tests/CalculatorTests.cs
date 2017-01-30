using Xunit;
using FluentAssertions;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        private readonly ICalculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void ItShouldOutput0ForNonNumericalString()
        {
            var result = _calculator.Calculate("notanumber");

            result.Should().Be(0);
        }

        [Theory]
        [InlineData("1,2", 3)]
        public void ItShouldReturnIntegerFromString(string input, int output)
        {
            var result = _calculator.Calculate(input);

            result.Should().Be(output);
        }
    }
}
