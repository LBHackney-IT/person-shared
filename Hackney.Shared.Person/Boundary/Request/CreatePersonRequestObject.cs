using System;
using System.Collections.Generic;
using Hackney.Shared.Person.Domain;

namespace Hackney.Shared.Person.Boundary.Request
{
    public class CreatePersonRequestObject
    {
        public Guid Id { get; set; }

        public Title? Title { get; set; }

        public Title? PreferredTitle { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredSurname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string PlaceOfBirth { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Reason { get; set; }
        public IEnumerable<PersonType> PersonTypes { get; set; }
        public IEnumerable<TenureDetails> Tenures { get; set; }
    }
}
