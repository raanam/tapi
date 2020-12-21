using System;
using System.Collections.Generic;
using System.Text;
using TrolleyApi.Tests.Common;
using TrolleyApi.User;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TrolleyApi.Tests.User
{
    public class UserServiceTests
    {
        private readonly IUserService _sut;

        public UserServiceTests()
        {
            var fixtrue = FixtureBuilder.Build();
            _sut = fixtrue.Create<UserService>();
        }

        [Fact]
        public void When_Get_Returns_ValidResponse()
        {
            // Act.
            var response = _sut.Get();

            // Assert.
            response.Name.Should().Be("Ram Anam");
            response.Token.Should().Be("dab78cfc-6bd8-428f-be75-19be7bd38643");
        }
    }
}
