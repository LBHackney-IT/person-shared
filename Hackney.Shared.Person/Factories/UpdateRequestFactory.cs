using Hackney.Shared.Person.Boundary.Request;
using Hackney.Shared.Person.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace Hackney.Shared.Person.Factories
{
    public static class UpdateRequestFactory
    {
        public static PersonDbEntity ToDatabase(this UpdatePersonRequestObject updatePersonRequestObject)
        {
            return new PersonDbEntity()
            {
                Title = updatePersonRequestObject.Title,
                PreferredTitle = updatePersonRequestObject.PreferredTitle,
                PreferredMiddleName = updatePersonRequestObject.PreferredMiddleName,
                PreferredFirstName = updatePersonRequestObject.PreferredFirstName,
                PreferredSurname = updatePersonRequestObject.PreferredSurname,
                FirstName = updatePersonRequestObject.FirstName,
                MiddleName = updatePersonRequestObject.MiddleName,
                Surname = updatePersonRequestObject.Surname,
                PlaceOfBirth = updatePersonRequestObject.PlaceOfBirth,
                DateOfBirth = updatePersonRequestObject.DateOfBirth,
                PersonTypes = null,
                Tenures = GetListOrNull(updatePersonRequestObject.Tenures)
            };
        }

        private static List<T> GetListOrNull<T>(IEnumerable<T> enumerable)
        {
            return enumerable == null ? null : enumerable.ToList();
        }
    }
}
