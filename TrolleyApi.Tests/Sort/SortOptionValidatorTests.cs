using FluentAssertions;
using TrolleyApi.Sort;
using Xunit;

namespace TrolleyApi.Tests.Sort
{
    public class SortOptionValidatorTests
    {
        [Theory]
        [InlineData("high")]
        [InlineData("low")]
        [InlineData("ascending")]
        [InlineData("descending")]
        [InlineData("recommended")]
        public void Given_ValidOptions_When_IsValid_Returns_True(string option)
        {
            // Arrange.
            var validator = new SortOptionValidator();

            // Act.
            var result = validator.IsValid(option);

            // Assert.
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("high1")]
        [InlineData("low2")]
        public void Given_InValidOptions_When_IsValid_Returns_False(string option)
        {
            // Arrange.
            var validator = new SortOptionValidator();

            // Act.
            var result = validator.IsValid(option);

            // Assert.
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(" high ")]
        [InlineData("low ")]
        public void Given_OptionsWithSpace_When_IsValid_Returns_True(string option)
        {
            // Arrange.
            var validator = new SortOptionValidator();

            // Act.
            var result = validator.IsValid(option);

            // Assert.
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("HigH")]
        [InlineData("loW")]
        public void Given_OptionsInIncorrectCase_When_IsValid_Returns_True(string option)
        {
            // Arrange.
            var validator = new SortOptionValidator();

            // Act.
            var result = validator.IsValid(option);

            // Assert.
            result.Should().BeTrue();
        }
    }
}
