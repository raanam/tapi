using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace TrolleyApi.Tests.Common
{
    public static class FixtureBuilder
    {
        public static IFixture Build()
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}
