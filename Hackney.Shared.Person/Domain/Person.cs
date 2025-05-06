using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Hackney.Shared.Person.Domain;

namespace Hackney.Shared.Person
{
    public class Person
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
        public int? VersionNumber { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public int PersonRef { get; set; }


        [JsonIgnore]
        public bool? IsAMinor
        {
            get
            {
                if (!DateOfBirth.HasValue || DateOfBirth.Value == DateTime.MinValue) return null;
                return DateOfBirth.Value.AddYears(18) > DateTime.UtcNow;
            }
        }
    }
}
