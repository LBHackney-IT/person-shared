using Hackney.Shared.Person.Boundary;
using Hackney.Shared.Person.Boundary.Response;
using Hackney.Shared.Person.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackney.Shared.Person.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        private readonly IApiLinkGenerator _apiLinkGenerator;
        public ResponseFactory(IApiLinkGenerator apiLinkGenerator)
        {
            _apiLinkGenerator = apiLinkGenerator;
        }

        public static string FormatDateOfBirth(DateTime? dob)
        {
            return dob?.ToString("yyyy-MM-dd");
        }

        public PersonResponseObject ToResponse(Person domain)
        {
            if (null == domain) return null;

            return new PersonResponseObject
            {
                Id = domain.Id,
                Title = domain.Title,
                PreferredTitle = domain.PreferredTitle,
                PreferredFirstName = domain.PreferredFirstName,
                PreferredMiddleName = domain.PreferredMiddleName,
                PreferredSurname = domain.PreferredSurname,
                FirstName = domain.FirstName,
                MiddleName = domain.MiddleName,
                Surname = domain.Surname,
                PlaceOfBirth = domain.PlaceOfBirth,
                DateOfBirth = FormatDateOfBirth(domain.DateOfBirth),
                PersonTypes = domain.PersonTypes,
                Links = _apiLinkGenerator?.GenerateLinksForPerson(domain),
                Tenures = SortTenures(domain.Tenures),
                Reason = domain.Reason,
                IsAMinor = domain.IsAMinor,
                DateOfDeath = domain.DateOfDeath,
                PersonRef = domain.PersonRef
            };
        }

        private static List<TenureResponseObject> SortTenures(IEnumerable<TenureDetails> tenures)
        {
            if (tenures == null) return null;

            var sortedTenures = tenures
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.Type == "Secure")
                .ThenByDescending(ParseTenureStartDate)
                .ToList();

            return sortedTenures.Select(x => ToResponse(x)).ToList();
        }

        private static DateTime? ParseTenureStartDate(TenureDetails tenure)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(tenure.StartDate, out parsedDate)) return (DateTime?)parsedDate;

            return null;
        }

        public static TenureResponseObject ToResponse(TenureDetails tenure)
        {
            return new TenureResponseObject()
            {
                AssetFullAddress = tenure.AssetFullAddress,
                AssetId = tenure.AssetId,
                EndDate = tenure.EndDate,
                Id = tenure.Id,
                IsActive = tenure.IsActive,
                PaymentReference = tenure.PaymentReference,
                PropertyReference = tenure.PropertyReference,
                StartDate = tenure.StartDate,
                Type = tenure.Type,
                Uprn = tenure.Uprn
            };
        }

    }
}
