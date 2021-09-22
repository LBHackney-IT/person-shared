using FluentValidation.TestHelper;
using Hackney.Shared.Person.Boundary.Request;
using Hackney.Shared.Person.Boundary.Request.Validation;
using System;
using Xunit;

namespace Hackney.Shared.Person.Tests.Boundary.Request.Validation
{
    public class PersonQueryObjectValidatorTests
    {
        private readonly PersonQueryObjectValidator _sut;

        public PersonQueryObjectValidatorTests()
        {
            _sut = new PersonQueryObjectValidator();
        }

        [Fact]
        public void QueryShouldErrorWithNullTargetId()
        {
            var query = new PersonQueryObject();
            var result = _sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void QueryShouldErrorWithEmptyTargetId()
        {
            var query = new PersonQueryObject() { Id = Guid.Empty };
            var result = _sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
