using Hackney.Shared.Person.Boundary.Response;

namespace Hackney.Shared.Person.Factories
{
    public interface IResponseFactory
    {
        PersonResponseObject ToResponse(Person domain);

    }
}
