using Xunit;
using FluentAssertions;
using System;

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
        [InlineData("4,5,5", 14)]
        public void ItShouldReturnIntegerFromString(string input, int output)
        {
            var result = _calculator.Calculate(input);

            result.Should().Be(output);
        }

        [Theory]
        [InlineData("1,fakenumber,3", 4)]
        [InlineData("1,,7", 8)]
        [InlineData("1,XyA%@$$%&()&^,7,1,2,3", 14)]
        public void ItShouldReturnIntegerWhenNonNumbersAreInTheInput(string input, int output)
        {
            var result = _calculator.Calculate(input);

            result.Should().Be(output);
        }

        [Fact]
        public void ShouldHandleNewLinesAswellAsCommas()
        {
            var result = _calculator.Calculate("1\n2,3");

            result.Should().Be(6);
        }

        [Theory]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//(\n1(2", 3)]
        public void ShouldAllowCustomDelimiters(string input, int output)
        {
            var result = _calculator.Calculate(input);

            result.Should().Be(output);
        }

        [Theory]
        [InlineData("1,2,3,-1", "-1")]
        [InlineData("1,2,3,-1,-2", "-1,-2")]
        public void ShouldThrowExceptionIfContainsNegitiveNumber(string input, string negatives)
        {
            var exception = Record.Exception(() => _calculator.Calculate(input));

            exception.Should().BeOfType<Exception>();

            exception.Message.Should().Be(string.Concat("negatives not allowed, passed: ", negatives));
        }
    }
}
