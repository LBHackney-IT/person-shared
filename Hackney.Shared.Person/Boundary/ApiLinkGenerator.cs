using Hackney.Shared.Person.Boundary.Response;
using System.Collections.Generic;
using System.Linq;

namespace Hackney.Shared.Person.Boundary
{
    public interface IApiLinkGenerator
    {
        IEnumerable<ApiLink> GenerateLinksForPerson(Person person);
    }

    public class ApiLinkGenerator : IApiLinkGenerator
    {
        public IEnumerable<ApiLink> GenerateLinksForPerson(Person person)
        {
            // TODO - Implement link generation
            return Enumerable.Empty<ApiLink>();
        }
    }
}
