using AutoFixture;
using FluentAssertions;
using Moq;
using Hackney.Shared.Person.Boundary;
using Hackney.Shared.Person.Domain;
using Hackney.Shared.Person.Factories;
using Hackney.Shared.Tenure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hackney.Shared.Person.Tests.Factories
{
    public class ResponseFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IApiLinkGenerator> _mockLinkGenerator;
        private readonly ResponseFactory _sut;

        private readonly Random _random = new Random();

        private readonly DateTime? _activeTenureDateValue = null;
        private readonly DateTime? _inactiveTeunureDateValue = DateTime.UtcNow.AddDays(-7); // generate date in past


        public ResponseFactoryTest()
        {
            _mockLinkGenerator = new Mock<IApiLinkGenerator>();
            _sut = new ResponseFactory(_mockLinkGenerator.Object);
        }

        [Fact]
        public void FormatDOBTestReturnsFormattedValue()
        {
            var dob = DateTime.UtcNow.AddYears(-30);
            var formatted = ResponseFactory.FormatDateOfBirth(dob);
            formatted.Should().Be(dob.ToString("yyyy-MM-dd"));
        }

        [Fact]
        public void FormatDOBTestReturnsFormattedNull()
        {
            var formatted = ResponseFactory.FormatDateOfBirth(null);
            formatted.Should().BeNull();
        }

        [Fact]
        public void NullPersonToResponseReturnsNull()
        {
            var response = _sut.ToResponse((Person)null);
            response.Should().BeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanMapADomainPersonToAResponsePerson(bool hasDob)
        {
            DateTime? dob = hasDob ? DateTime.UtcNow.AddYears(-30) : default;
            var person = _fixture.Build<Person>()
                                 .With(x => x.DateOfBirth, dob)
                                 .Create();
            var response = _sut.ToResponse(person);

            response.Id.Should().Be(person.Id);
            response.Title.Should().Be(person.Title);
            response.PreferredFirstName.Should().Be(person.PreferredFirstName);
            response.PreferredSurname.Should().Be(person.PreferredSurname);
            response.FirstName.Should().Be(person.FirstName);
            response.MiddleName.Should().Be(person.MiddleName);
            response.Surname.Should().Be(person.Surname);
            response.PlaceOfBirth.Should().Be(person.PlaceOfBirth);
            response.DateOfBirth.Should().Be(ResponseFactory.FormatDateOfBirth(person.DateOfBirth));
            response.PersonTypes.Should().BeEquivalentTo(person.PersonTypes);
        }

        [Fact]
        public void PersonResponseObjectToResponseWhenCalledOrdersActiveTenuresBeforeInactiveTenures()
        {
            int numberOfActiveTenures = _random.Next(2, 5);
            var numberOfInactiveTenures = _random.Next(2, 5);

            // create many active tenures
            var activeTenures = _fixture.Build<TenureInformation>().With(x => x.EndOfTenureDate, _activeTenureDateValue).CreateMany(numberOfActiveTenures);
            // create many inactive tenures
            var inactiveTenures = _fixture.Build<TenureInformation>().With(x => x.EndOfTenureDate, _inactiveTeunureDateValue).CreateMany(numberOfInactiveTenures);

            // shuffle list
            var shuffledTenures = ShuffleTenures(activeTenures.Concat(inactiveTenures));

            // create mock person
            var mockPerson = _fixture.Build<Person>().With(x => x.Tenures, shuffledTenures).Create();

            // call method
            var response = _sut.ToResponse(mockPerson);

            var responseActiveTenures = response.Tenures.Take(numberOfActiveTenures);
            var responseInactiveTenures = response.Tenures.Skip(numberOfActiveTenures).Take(numberOfInactiveTenures);

            // assert first half are active
            responseActiveTenures.Should().OnlyContain(x => x.IsActive == true);

            // assert second half are inactive
            responseInactiveTenures.Should().OnlyContain(x => x.IsActive == false);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PersonResponseObjectToResponseWhenCalledOrdersSecureTenuresBeforeOtherTypes(bool activeTenures)
        {
            var tenureEndDate = activeTenures ? _activeTenureDateValue : _inactiveTeunureDateValue;

            int numberOfSecureTenures = _random.Next(2, 5);
            var numberOfOtherTenureTypes = _random.Next(2, 5);

            // create many secure tenures
            var secureTenures = _fixture.Build<TenureInformation>().With(x => x.EndOfTenureDate, tenureEndDate).With(x => x.TenureType, TenureTypes.Secure).CreateMany(numberOfSecureTenures);

            // create many tenures of other types
            var otherTenureTypes = _fixture.Build<TenureInformation>().With(x => x.EndOfTenureDate, tenureEndDate).CreateMany(numberOfOtherTenureTypes);

            // shuffle list
            var shuffledTenures = ShuffleTenures(secureTenures.Concat(otherTenureTypes));

            // mock person
            var mockPerson = _fixture.Build<Person>().With(x => x.Tenures, shuffledTenures).Create();

            // call method
            var response = _sut.ToResponse(mockPerson);

            var responseSecureTenures = response.Tenures.Take(numberOfSecureTenures);
            var responseOtherTenureTypes = response.Tenures.Skip(numberOfSecureTenures).Take(numberOfOtherTenureTypes);

            // assert first half are secure
            responseSecureTenures.Should().OnlyContain(x => x.TenureType.Description == "Secure");

            // assert second half arent secure
            responseOtherTenureTypes.Should().NotContain(x => x.TenureType.Description == "Secure");
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void PersonResponseObjectToResponseWhenCalledOrdersTenuresByDate(bool activeTenures)
        {
            var tenureEndDate = activeTenures ? _activeTenureDateValue : _inactiveTeunureDateValue;

            int numberOfSecureTenures = _random.Next(5, 10);
            var numberOfOtherTenureTypes = _random.Next(5, 10);

            // create many secure tenures
            var secureTenures = _fixture.Build<TenureInformation>()
                .With(x => x.EndOfTenureDate, tenureEndDate)
                .With(x => x.TenureType, TenureTypes.Secure)
                .With(x => x.StartOfTenureDate, CreateRandomStartDateValue())
                .CreateMany(numberOfSecureTenures);

            // create many tenures of other types
            var otherTenureTypes = _fixture
                .Build<TenureInformation>()
                .With(x => x.EndOfTenureDate, tenureEndDate)
                .With(x => x.StartOfTenureDate, CreateRandomStartDateValue())
                .CreateMany(numberOfOtherTenureTypes);

            // shuffle list
            var shuffledTenures = ShuffleTenures(secureTenures.Concat(otherTenureTypes));

            // mock person
            var mockPerson = _fixture.Build<Person>().With(x => x.Tenures, shuffledTenures).Create();

            // call method
            var response = _sut.ToResponse(mockPerson);

            var responseSecureTenures = response.Tenures.Take(numberOfSecureTenures);
            var responseOtherTenureTypes = response.Tenures.Skip(numberOfSecureTenures).Take(numberOfOtherTenureTypes);

            // assert tenures tenures are in date order
            responseSecureTenures.Select(x => x.StartOfTenureDate).Should().BeInDescendingOrder();
            responseOtherTenureTypes.Select(x => x.StartOfTenureDate).Should().BeInDescendingOrder();
        }

        private IEnumerable<TenureInformation> ShuffleTenures(IEnumerable<TenureInformation> list)
        {
            Random random = new Random();

            return list.OrderBy(item => random.Next());
        }

        private DateTime CreateRandomStartDateValue()
        {
            // An inactive tenure must have enddate set in past
            var numberOfDaysInPast = _random.Next(-1000, -1);

            return DateTime.UtcNow.AddDays(numberOfDaysInPast);
        }
    }
}
