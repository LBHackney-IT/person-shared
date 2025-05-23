using AutoFixture;
using FluentAssertions;
using Hackney.Shared.Person;
using Hackney.Shared.Person.Infrastructure;
using Hackney.Shared.Person.Factories;
using Xunit;

namespace Hackney.Shared.Person.Tests
{
    public class EntityFactoryTest
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = _fixture.Build<PersonDbEntity>()
                                         .Create();
            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.Title.Should().Be(entity.Title);
            databaseEntity.PreferredFirstName.Should().Be(entity.PreferredFirstName);
            databaseEntity.PreferredSurname.Should().Be(entity.PreferredSurname);
            databaseEntity.FirstName.Should().Be(entity.FirstName);
            databaseEntity.MiddleName.Should().Be(entity.MiddleName);
            databaseEntity.Surname.Should().Be(entity.Surname);
            databaseEntity.PlaceOfBirth.Should().Be(entity.PlaceOfBirth);
            databaseEntity.DateOfBirth.Should().Be(entity.DateOfBirth);
            databaseEntity.Tenures.Should().BeEquivalentTo(entity.Tenures);
            databaseEntity.DateOfDeath.Should().Be(entity.DateOfDeath);
            databaseEntity.PersonRef.Should().Be(entity.PersonRef);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var person = _fixture.Build<Person>()
                                 .Create();
            var databaseEntity = person.ToDatabase();

            person.Id.Should().Be(databaseEntity.Id);
            person.Title.Should().Be(person.Title);
            person.PreferredFirstName.Should().Be(databaseEntity.PreferredFirstName);
            person.PreferredSurname.Should().Be(databaseEntity.PreferredSurname);
            person.FirstName.Should().Be(databaseEntity.FirstName);
            person.MiddleName.Should().Be(databaseEntity.MiddleName);
            person.Surname.Should().Be(databaseEntity.Surname);
            person.PlaceOfBirth.Should().Be(databaseEntity.PlaceOfBirth);
            person.DateOfBirth.Should().Be(databaseEntity.DateOfBirth);
            person.PersonTypes.Should().BeEquivalentTo(databaseEntity.PersonTypes);
            person.Tenures.Should().BeEquivalentTo(databaseEntity.Tenures);
            person.DateOfDeath.Should().Be(databaseEntity.DateOfDeath);
            person.PersonRef.Should().Be(databaseEntity.PersonRef);
        }
    }
}
