using AutoFixture;
using FluentAssertions;
using Force.DeepCloner;
using Hackney.Shared.Person.Domain;
using System;
using System.Linq;
using Xunit;

namespace Hackney.Shared.Person.Tests.Domain
{
    public class TenureTests
    {
        private readonly Fixture _fixture = new Fixture();
        private TenureDetails _classUnderTest;

        public TenureTests()
        {
            _classUnderTest = new Shared.Person.Domain.TenureDetails();
        }

        [Fact]
        public void GivenATenureWhenEndDateIsNullThenIsActiveShouldBeTrue()
        {
            // given + when + then
            _classUnderTest.IsActive.Should().BeTrue();
        }

        [Fact]
        public void GivenATenureWhenEndDateIsGreaterThanCurrentDateThenIsActiveShouldBeTrue()
        {
            // given
            _classUnderTest.EndDate = DateTime.Now.AddDays(1).ToShortDateString();

            // when + then
            _classUnderTest.IsActive.Should().BeTrue();
        }

        [Fact]
        public void GivenATenureWhenEndDateIsMinimumDateThanCurrentDateThenIsActiveShouldBeTrue()
        {
            // given
            _classUnderTest.EndDate = "1900-01-01";

            // when + then
            _classUnderTest.IsActive.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenATenureWhenEndDateIsLessThanCurrentDateThenIsActiveShouldBeFalse(int daysToAdd)
        {
            // given
            _classUnderTest.EndDate = DateTime.Now.AddDays(daysToAdd).ToShortDateString();

            // when + then
            _classUnderTest.IsActive.Should().BeFalse();
        }

        [Fact]
        public void EqualsTestWrongTypeReturnsFalse()
        {
            _classUnderTest.Equals("some string").Should().BeFalse();
        }

        [Fact]
        public void EqualsTestSameObjectReturnsTrue()
        {
            _classUnderTest = _fixture.Create<TenureDetails>();
            _classUnderTest.Equals(_classUnderTest).Should().BeTrue();
        }

        [Fact]
        public void EqualsTestEquivalentObjectReturnsTrue()
        {
            _classUnderTest = _fixture.Create<TenureDetails>();
            var compare = _classUnderTest.DeepClone();
            _classUnderTest.Equals(compare).Should().BeTrue();
        }

        [Fact]
        public void EqualsTestDifferentObjectReturnsFalse()
        {
            _classUnderTest = _fixture.Create<TenureDetails>();
            var compare = _fixture.Create<TenureDetails>();
            _classUnderTest.Equals(compare).Should().BeFalse();
        }

        [Fact]
        public void EqualsTestNullObjectReturnsFalse()
        {
            _classUnderTest = _fixture.Create<TenureDetails>();
            _classUnderTest.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void EqualsTestWithNullsEquivalentObjectReturnsTrue()
        {
            _classUnderTest = new TenureDetails();
            var compare = new TenureDetails();
            _classUnderTest.Equals(compare).Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeTest()
        {
            _classUnderTest = _fixture.Create<TenureDetails>();
            var propValues = _classUnderTest.GetType()
                                .GetProperties()
                                .Select(x => x.GetValue(_classUnderTest).ToString());
            var expected = string.Join("", propValues).GetHashCode();
            _classUnderTest.GetHashCode().Should().Be(expected);
        }
    }
}
