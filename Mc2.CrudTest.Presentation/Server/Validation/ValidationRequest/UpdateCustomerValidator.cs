using FluentValidation;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;

namespace Mc2.CrudTest.Presentation.Server.Validation.ValidationRequest
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Firstname)
                .NotNull()
                .NotEmpty()
                .WithMessage("First name could not be empty");


            RuleFor(x => x.Lastname)
                .MinimumLength(3)
                .WithMessage("last name must be a string with a minimum length of '3'.");


            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("email could not be empty");


            RuleFor(x => x.Email.ToString())
                .MinimumLength(3)
                .WithMessage("Email must be a string with a minimum length of '3'.");


            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("phone number could not be empty");

        }
    }
}