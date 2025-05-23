using Hackney.Shared.Person.Domain;
using System;
using System.Collections.Generic;

namespace Hackney.Shared.Person.Tests.Helper
{
    public static class Constants
    {
        public static Guid ID { get; } = Guid.NewGuid();
        public const Title TITLE = Title.Mr;
        public const Title PREFTITLE = Title.Mr;
        public const string PREFFIRSTNAME = "Bob";
        public const string PREFMIDDLENAME = "Tim";
        public const string PREFSURNAME = "Roberts";
        public const string FIRSTNAME = "Robert";
        public const string MIDDLENAME = "Tim";
        public const string SURNAME = "Roberts";
        public const string PLACEOFBIRTH = "London";
        public static DateTime DATEOFBIRTH { get; } = DateTime.UtcNow.AddYears(-40);
        public static DateTime DATEOFDEATH { get; } = DateTime.UtcNow.AddYears(40);


        public static Guid TENUREID = Guid.NewGuid();
        public const string SOMEUPRN = "SomeUprn";
        public const string SOMETYPE = "Block";
        public static string ASSETID = Guid.NewGuid().ToString();
        public const string ASSETFULLADDRESS = "SomeAddress";
        public const string STARTDATE = "2012-07-19";
        public const string ENDDATE = "2015-07-19";
        public const string PAYMENTREF = "123456789";
        public const string PROPERTYREF = "987654321";
        public const int PERSONREF = 70000000;

        public static IEnumerable<PersonType> PERSONTYPES { get; }
            = new List<PersonType> { PersonType.HouseholdMember };

        public static Person ConstructPersonFromConstants()
        {
            var entity = new Person();
            entity.Id = Constants.ID;
            entity.Title = Constants.TITLE;
            entity.PreferredTitle = Constants.PREFTITLE;
            entity.PreferredFirstName = Constants.PREFFIRSTNAME;
            entity.PreferredMiddleName = Constants.PREFMIDDLENAME;
            entity.PreferredSurname = Constants.PREFSURNAME;
            entity.FirstName = Constants.FIRSTNAME;
            entity.MiddleName = Constants.MIDDLENAME;
            entity.Surname = Constants.SURNAME;
            entity.PlaceOfBirth = Constants.PLACEOFBIRTH;
            entity.DateOfBirth = Constants.DATEOFBIRTH;
            entity.DateOfDeath = Constants.DATEOFDEATH;
            entity.Tenures = new[]
            {
                new Shared.Person.Domain.TenureDetails
                {
                    AssetFullAddress = ASSETFULLADDRESS,
                    AssetId = ASSETID,
                    EndDate = ENDDATE,
                    StartDate = STARTDATE,
                    Id = TENUREID,
                    Type = SOMETYPE,
                    PaymentReference = PAYMENTREF,
                    PropertyReference = PROPERTYREF,
                    Uprn = SOMEUPRN
                }
            };
            entity.PersonTypes = Constants.PERSONTYPES;
            entity.PersonRef = Constants.PERSONREF;
            return entity;
        }
    }
}
