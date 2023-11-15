using FluentValidation.Results;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;
using Mc2.CrudTest.Presentation.Server.Validation.ValidationRequest;

namespace Mc2.CrudTest.Presentation.Server.Validation.Customers
{
    public static class CustomerValidationRules
    {
        public static ValidationResult CreateValidation(CreateCustomerRequest request)
        {
            var validation = new CreateCustomerValidator();

            return  validation.Validate(request);
        }
    }
}