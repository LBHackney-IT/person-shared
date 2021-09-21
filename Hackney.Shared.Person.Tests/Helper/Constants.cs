using Hackney.Shared.Person.Domain;
using System;
using System.Collections.Generic;
using Hackney.Shared.Tenure.Domain;

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
        public static Guid TENUREID = Guid.NewGuid();
        public const string SOMEUPRN = "SomeUprn";
        public const TenuredAssetType SOMETYPE = TenuredAssetType.Block;
        public static Guid ASSETID = Guid.NewGuid();
        public const string ASSETFULLADDRESS = "SomeAddress";
        public static DateTime STARTDATE = Convert.ToDateTime("2012-07-19");
        public static DateTime ENDDATE = Convert.ToDateTime("2015-07-19");

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
            entity.Tenures = new[]
            {
                new TenureInformation
                {
                    Id = TENUREID,
                    StartOfTenureDate = STARTDATE,
                    EndOfTenureDate = ENDDATE,
                    TenuredAsset = new TenuredAsset
                    {
                        Id = ASSETID,
                        Type = SOMETYPE,
                        FullAddress = ASSETFULLADDRESS,
                        Uprn = SOMEUPRN
                    }
                }
            };
            entity.PersonTypes = Constants.PERSONTYPES;
            return entity;
        }
    }
}
