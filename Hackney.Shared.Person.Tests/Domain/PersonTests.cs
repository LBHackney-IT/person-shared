using FluentAssertions;
using Hackney.Shared.Person.Tests.Helper;
using System;
using System.Linq;
using Xunit;

namespace Hackney.Shared.Person.Tests.Domain
{
    public class PersonTests
    {
        [Fact]
        public void PersonHasPropertiesSet()
        {
            Person person = Constants.ConstructPersonFromConstants();

            person.Id.Should().Be(Constants.ID);
            person.Title.Should().Be(Constants.TITLE);
            person.PreferredTitle.Should().Be(Constants.PREFTITLE);
            person.PreferredFirstName.Should().Be(Constants.PREFFIRSTNAME);
            person.PreferredMiddleName.Should().Be(Constants.PREFMIDDLENAME);
            person.PreferredSurname.Should().Be(Constants.PREFSURNAME);
            person.FirstName.Should().Be(Constants.FIRSTNAME);
            person.MiddleName.Should().Be(Constants.MIDDLENAME);
            person.Surname.Should().Be(Constants.SURNAME);
            person.PlaceOfBirth.Should().Be(Constants.PLACEOFBIRTH);
            person.DateOfBirth.Should().Be(Constants.DATEOFBIRTH);
            person.PersonTypes.Should().BeEquivalentTo(Constants.PERSONTYPES);
            person.Tenures.Should().ContainSingle();
            person.Tenures.First().Id.Should().Be(Constants.TENUREID);
            person.Tenures.First().AssetId.Should().Be(Constants.ASSETID.ToString());
            person.Tenures.First().AssetFullAddress.Should().Be(Constants.ASSETFULLADDRESS);
            person.Tenures.First().StartDate.Should().Be(Constants.STARTDATE);
            person.Tenures.First().EndDate.Should().Be(Constants.ENDDATE);
            person.Tenures.First().Type.Should().Be(Constants.SOMETYPE);
            person.Tenures.First().PaymentReference.Should().Be(Constants.PAYMENTREF);
            person.Tenures.First().PropertyReference.Should().Be(Constants.PROPERTYREF);
            person.Tenures.First().Uprn.Should().Be(Constants.SOMEUPRN);
        }

        [Fact]
        public void PersonIsAMinorTestNoDoBReturnsNull()
        {
            var person = new Person();
            person.IsAMinor.Should().BeNull();

            person.DateOfBirth = DateTime.MinValue;
            person.IsAMinor.Should().BeNull();
        }

        [Fact]
        public void PersonIsAMinorTestReturnsFalse()
        {
            var person = new Person();
            person.DateOfBirth = DateTime.UtcNow.AddYears(-18);
            person.IsAMinor.Should().BeFalse();

            person.DateOfBirth = DateTime.UtcNow.AddYears(-38);
            person.IsAMinor.Should().BeFalse();
        }

        [Fact]
        public void PersonIsAMinorTestReturnsTrue()
        {
            var person = new Person();
            person.DateOfBirth = DateTime.UtcNow.AddMinutes(-1);
            person.IsAMinor.Should().BeTrue();

            person.DateOfBirth = DateTime.UtcNow.AddYears(-1);
            person.IsAMinor.Should().BeTrue();

            person.DateOfBirth = DateTime.UtcNow.AddYears(-17);
            person.IsAMinor.Should().BeTrue();
        }
    }
}
