using Amazon.DynamoDBv2.DataModel;
using Hackney.Core.DynamoDb.Converters;
using System;
using System.Collections.Generic;
using Hackney.Shared.Person.Domain;
using Hackney.Shared.Tenure.Domain;

namespace Hackney.Shared.Person.Infrastructure
{
    [DynamoDBTable("Persons", LowerCamelCaseProperties = true)]
    public class PersonDbEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty(Converter = typeof(DynamoDbEnumConverter<Title>))]
        public Title? Title { get; set; }
        [DynamoDBProperty(Converter = typeof(DynamoDbEnumConverter<Title>))]
        public Title? PreferredTitle { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredSurname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Reason { get; set; }

        [DynamoDBProperty(Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }

        [DynamoDBProperty(Converter = typeof(DynamoDbEnumListConverter<PersonType>))]
        public List<PersonType> PersonTypes { get; set; } = new List<PersonType>();

        [DynamoDBProperty(Converter = typeof(DynamoDbObjectListConverter<TenureInformation>))]
        public List<TenureInformation> Tenures { get; set; } = new List<TenureInformation>();

        [DynamoDBVersion]
        public int? VersionNumber { get; set; }

        [DynamoDBProperty(Converter = typeof(DynamoDbDateTimeConverter))]
        public DateTime? LastModified { get; set; }
    }
}
