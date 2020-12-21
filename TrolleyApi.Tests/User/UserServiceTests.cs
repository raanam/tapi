using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TrolleyApi.Tests.Common;
using TrolleyApi.User;
using Xunit;

namespace TrolleyApi.Tests.User
{
    public class UserServiceTests
    {
        private readonly IUserService _sut;
        private readonly IConfiguration _configuration;

        public UserServiceTests()
        {
            var fixtrue = FixtureBuilder.Build();
            _configuration = fixtrue.Freeze<IConfiguration>();
            _sut = fixtrue.Create<UserService>();
        }

        [Fact]
        public void When_Get_Returns_ValidResponse()
        {
            // Arrange.
            _configuration["UserToken"].Returns("User-Token");

            // Act.
            var response = _sut.Get();

            // Assert.
            response.Name.Should().Be("Ram Anam");
            response.Token.Should().Be("User-Token");
        }
    }
}
