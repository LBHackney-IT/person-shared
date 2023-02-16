using FluentValidation;
using Hackney.Core.Validation;
using Hackney.Shared.Person.Domain;
using System;
using System.Linq;

namespace Hackney.Shared.Person.Boundary.Request.Validation
{
    public class CreatePersonRequestObjectValidator : AbstractValidator<CreatePersonRequestObject>
    {
        public CreatePersonRequestObjectValidator()
        {
            RuleFor(x => x.PersonTypes).NotNull()
                                       .NotEmpty()
                                       .WithErrorCode(ErrorCodes.PersonTypeMandatory);

            RuleFor(x => x.Title).IsInEnum();

            //Title should be nullable for HousingOfficer and HousingAreaManager PersonTypes. 
            RuleFor(x => x.Title).NotNull().When(x => x.PersonTypes?.Contains(PersonType.HousingOfficer) == false)
                                           .When(x => x.PersonTypes?.Contains(PersonType.HousingAreaManager) == false);

            RuleFor(x => x.DateOfBirth).NotEqual(default(DateTime))
                                       .WithErrorCode(ErrorCodes.DoBInvalid);
            RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow)
                                       .WithErrorCode(ErrorCodes.DoBInFuture);

            //DOB should be nullable for HousingOfficer and HousingAreaManager PersonTypes
            RuleFor(x => x.DateOfBirth).NotNull().When(x => x.PersonTypes?.Contains(PersonType.HousingOfficer) == false)
                                                 .When(x => x.PersonTypes?.Contains(PersonType.HousingAreaManager) == false);

            RuleFor(x => x.FirstName).NotNull()
                                     .NotEmpty()
                                     .WithErrorCode(ErrorCodes.FirstNameMandatory);
            RuleFor(x => x.FirstName).NotXssString()
                                     .WithErrorCode(ErrorCodes.XssCheckFailure);

            RuleFor(x => x.Surname).NotNull()
                                   .NotEmpty()
                                   .WithErrorCode(ErrorCodes.SurnameMandatory);
            RuleFor(x => x.Surname).NotXssString()
                                   .WithErrorCode(ErrorCodes.XssCheckFailure);

            
            RuleForEach(x => x.PersonTypes)
                .ChildRules(x => x.RuleFor(y => y).IsInEnum());

            RuleFor(x => x.Reason).NotNull()
                                  .NotEmpty()
                                  .WithErrorCode(ErrorCodes.ReasonMandatory);
            RuleFor(x => x.Reason).NotXssString()
                                  .WithErrorCode(ErrorCodes.XssCheckFailure);

            // Others
            RuleFor(x => x.MiddleName)
                .NotXssString()
                .WithErrorCode(ErrorCodes.XssCheckFailure)
                .When(y => !string.IsNullOrEmpty(y.MiddleName));
            RuleFor(x => x.PreferredTitle).IsInEnum()
                .When(y => y.PreferredTitle != null);
            RuleFor(x => x.PreferredFirstName)
                .NotXssString()
                .WithErrorCode(ErrorCodes.XssCheckFailure)
                .When(y => !string.IsNullOrEmpty(y.PreferredFirstName));
            RuleFor(x => x.PreferredMiddleName)
                .NotXssString()
                .WithErrorCode(ErrorCodes.XssCheckFailure)
                .When(y => !string.IsNullOrEmpty(y.PreferredMiddleName));
            RuleFor(x => x.PreferredSurname)
                .NotXssString()
                .WithErrorCode(ErrorCodes.XssCheckFailure)
                .When(y => !string.IsNullOrEmpty(y.PreferredSurname));
            RuleFor(x => x.PlaceOfBirth)
                .NotXssString()
                .WithErrorCode(ErrorCodes.XssCheckFailure)
                .When(y => !string.IsNullOrEmpty(y.PlaceOfBirth));
            RuleForEach(x => x.Tenures).SetValidator(new TenureValidator());
        }
    }
}
