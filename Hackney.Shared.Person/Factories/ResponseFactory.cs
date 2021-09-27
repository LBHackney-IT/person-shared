using Hackney.Shared.Person.Boundary;
using Hackney.Shared.Person.Boundary.Response;
using Hackney.Shared.Tenure.Boundary.Response;
using Hackney.Shared.Tenure.Domain;
using Hackney.Shared.Tenure.Factories;
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
                Reason = domain.Reason
            };
        }

        private static List<TenureResponseObject> SortTenures(IEnumerable<TenureInformation> tenures)
        {
            if (tenures == null) return null;

            var sortedTenures = tenures
                .OrderByDescending(x => x.IsActive)
                .ThenByDescending(x => x.TenureType.Description == "Secure")
                .ThenByDescending(x => x.StartOfTenureDate)
                .ToList();

            return sortedTenures.Select(x => x.ToResponse()).ToList();
        }

    }
}
