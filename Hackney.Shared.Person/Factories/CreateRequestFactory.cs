using Hackney.Shared.Person.Boundary.Request;
using Hackney.Shared.Person.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackney.Shared.Person.Factories
{
    public static class CreateRequestFactory
    {
        public static PersonDbEntity ToDatabase(this CreatePersonRequestObject createPersonRequestObject)
        {
            return new PersonDbEntity()
            {
                Id = createPersonRequestObject.Id == Guid.Empty ? Guid.NewGuid() : createPersonRequestObject.Id,
                Title = createPersonRequestObject?.Title,
                PreferredTitle = createPersonRequestObject.PreferredTitle,
                PreferredMiddleName = createPersonRequestObject.PreferredMiddleName,
                PreferredFirstName = createPersonRequestObject.PreferredFirstName,
                PreferredSurname = createPersonRequestObject.PreferredSurname,
                FirstName = createPersonRequestObject.FirstName,
                MiddleName = createPersonRequestObject.MiddleName,
                Surname = createPersonRequestObject.Surname,
                PlaceOfBirth = createPersonRequestObject.PlaceOfBirth,
                DateOfBirth = createPersonRequestObject?.DateOfBirth,
                Reason = createPersonRequestObject.Reason,
                PersonTypes = GetListOrEmpty(createPersonRequestObject.PersonTypes),
                Tenures = GetListOrEmpty(createPersonRequestObject.Tenures)
            };
        }

        private static List<T> GetListOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable == null ? new List<T>() : enumerable.ToList();
        }
    }
}
