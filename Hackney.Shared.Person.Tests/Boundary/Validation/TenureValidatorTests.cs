using FluentValidation.TestHelper;
using Hackney.Shared.Person.Boundary.Request.Validation;
using Hackney.Shared.Person.Domain;
using System;
using Xunit;

namespace Hackney.Shared.Person.Tests.Boundary.Validation
{
    public class TenureValidatorTests
    {
        private readonly TenureValidator _sut;

        public TenureValidatorTests()
        {
            _sut = new TenureValidator();
        }

        [Theory]
        [InlineData("10 Some month 2001")]
        public void StartDateShouldErrorWithInvalidValue(string invalid)
        {
            var model = new TenureDetails() { StartDate = invalid };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.StartDate);
        }

        [Theory]
        [InlineData("10 Some month 2001")]
        public void EndDateShouldErrorWithInvalidValue(string invalid)
        {
            var model = new TenureDetails() { EndDate = invalid };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.EndDate);
        }

        [Fact]
        public void ShouldErrorWithEmptyId()
        {
            var query = new TenureDetails() { Id = Guid.Empty };
            var result = _sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }

        [Fact]
        public void PropertiesShouldErrorWithTagsInValue()
        {
            string value = "Some string with <tag> in it.";
            var model = new TenureDetails()
            {
                AssetFullAddress = value,
                AssetId = value,
                Type = value,
                PropertyReference = value,
                PaymentReference = value,
                Uprn = value
            };
            var result = _sut.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AssetFullAddress)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
            result.ShouldHaveValidationErrorFor(x => x.AssetId)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
            result.ShouldHaveValidationErrorFor(x => x.Type)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
            result.ShouldHaveValidationErrorFor(x => x.PropertyReference)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
            result.ShouldHaveValidationErrorFor(x => x.PaymentReference)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
            result.ShouldHaveValidationErrorFor(x => x.Uprn)
                  .WithErrorCode(ErrorCodes.XssCheckFailure);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void PropertiesShouldNotErrorWithNoValue(string value)
        {
            var model = new TenureDetails()
            {
                AssetFullAddress = value,
                AssetId = value,
                StartDate = value,
                EndDate = value,
                Type = value,
                PropertyReference = value,
                PaymentReference = value,
                Uprn = value
            };
            var result = _sut.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.AssetFullAddress);
            result.ShouldNotHaveValidationErrorFor(x => x.AssetId);
            result.ShouldNotHaveValidationErrorFor(x => x.StartDate);
            result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
            result.ShouldNotHaveValidationErrorFor(x => x.Type);
            result.ShouldNotHaveValidationErrorFor(x => x.PropertyReference);
            result.ShouldNotHaveValidationErrorFor(x => x.PaymentReference);
            result.ShouldNotHaveValidationErrorFor(x => x.Uprn);
        }
    }
}
