using FluentAssertions;
using Hackney.Shared.Person.Tests.Helper;
using Hackney.Shared.Person;
using System.Linq;
using Xunit;

namespace Hackney.Shared.Person.Tests
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
            person.Tenures.First().TenuredAsset.Id.Should().Be(Constants.ASSETID);
            person.Tenures.First().TenuredAsset.FullAddress.Should().Be(Constants.ASSETFULLADDRESS);
            person.Tenures.First().TenuredAsset.Type.Should().Be(Constants.SOMETYPE);
        }
    }
}
