using FluentValidation;
using System;

namespace Hackney.Shared.Person.Boundary.Request.Validation
{
    public class PersonQueryObjectValidator : AbstractValidator<PersonQueryObject>
    {
        public PersonQueryObjectValidator()
        {
            RuleFor(x => x.Id).NotNull()
                              .NotEqual(Guid.Empty);
        }
    }
}
