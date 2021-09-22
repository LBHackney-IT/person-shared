using AutoFixture;
using FluentAssertions;
using Hackney.Shared.Person.Boundary.Request;
using Hackney.Shared.Person.Factories;
using Xunit;

namespace Hackney.Shared.Person.Tests.Boundary
{
    public class UpdatePersonRequestObjectTests
    {
        [Fact]
        public void ToDatabaseTestNullSubObjectsCreatesNull()
        {
            var result = (new UpdatePersonRequestObject()).ToDatabase();
            result.Tenures.Should().BeNull();
        }

        [Fact]
        public void ToDatabaseTestSubObjectsAreEqual()
        {
            var request = new Fixture().Create<UpdatePersonRequestObject>();
            var result = request.ToDatabase();
            result.Tenures.Should().BeEquivalentTo(request.Tenures);
        }
    }
}
