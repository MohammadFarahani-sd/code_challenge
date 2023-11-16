using Azure;
using Mc2.CrudTest.Application.Contract.Customers.Command;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;
using Mc2.CrudTest.Presentation.Server.Validation.Customers;
using Microsoft.AspNetCore.Mvc;

using MediatR;
namespace Mc2.CrudTest.Presentation.Server.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCustomerRequest request)
    {
        var validateRider = CustomerValidationRules.CreateValidation(request);

        if (!validateRider.IsValid)
        {
            var currentRiderExceptions = validateRider.Errors.Select(f => $"{f}").ToList();
            throw new BadHttpRequestException(string.Join(",", currentRiderExceptions));
        }

        var customerCreated = _mediator.Send(new CreateCustomerCommand(request.Firstname, request.Lastname,
            request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber));
        return Created($"{Request.Path}/{customerCreated}", customerCreated);
    }

  
}