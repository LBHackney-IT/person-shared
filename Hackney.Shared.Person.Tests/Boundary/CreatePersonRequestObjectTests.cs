using AutoFixture;
using FluentAssertions;
using Hackney.Shared.Person.Boundary.Request;
using Hackney.Shared.Person.Factories;
using Hackney.Shared.Person;
using System;
using Xunit;

namespace Hackney.Shared.Person.Tests.Boundary
{
    public class CreatePersonRequestObjectTests
    {
        [Fact]
        public void ToDatabaseTestEmptyGuidCreatesNewGuid()
        {
            var result = (new CreatePersonRequestObject()).ToDatabase();
            result.Id.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void ToDatabaseTestNullSubObjectsCreatesDefaults()
        {
            var result = (new CreatePersonRequestObject()).ToDatabase();
            result.PersonTypes.Should().NotBeNull()
                                   .And.BeEmpty();
            result.Tenures.Should().NotBeNull()
                               .And.BeEmpty();
        }

        [Fact]
        public void ToDatabaseTestSubObjectsAreEqual()
        {
            var request = new Fixture().Create<CreatePersonRequestObject>();
            var result = request.ToDatabase();
            result.PersonTypes.Should().BeEquivalentTo(request.PersonTypes);
            result.Tenures.Should().BeEquivalentTo(request.Tenures);
        }
    }
}
