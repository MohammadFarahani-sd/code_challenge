using System.Net.Mail;
using Azure;
using Common.Helpers;
using Mc2.CrudTest.Application.Contract.Customers.Command;
using Mc2.CrudTest.Presentation.Server.Controllers.Models;
using Mc2.CrudTest.Presentation.Server.Validation.Customers;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using Mc2.CrudTest.Query.Queries.Customers;
using Mc2.CrudTest.Query.Models.Customers.Response;
using Mc2.CrudTest.Query.Filters;
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


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<CustomerResponse>> GetCustomerAsync([FromRoute] Guid id)
    {
        var customer = await _mediator.Send(new GetCustomerQuery(id));
        return Ok(customer);

    }


    [HttpGet]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<List<CustomerResponse>>> GetCustomersAsync([FromQuery] CustomerFilter request)
    {
        var customers = await _mediator.Send(new GetCustomersQuery(request));
        return Ok(customers);

    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCustomerRequest request)
    {
        try
        {

            var phone = PhoneNumbersExtensions.TryToGetFromPhoneNumber(request.PhoneNumber);
            var validateRider = CustomerValidationRules.CreateValidation(request);

            if (!validateRider.IsValid)
            {
                var currentRiderExceptions = validateRider.Errors.Select(f => $"{f}").ToList();
                throw new BadHttpRequestException(string.Join(",", currentRiderExceptions));
            }

            var customerCreated = await _mediator.Send(new CreateCustomerCommand(request.Firstname, request.Lastname,
                request.DateOfBirth, ulong.Parse(phone), new MailAddress(request.Email), request.BankAccountNumber));
            return Created($"", customerCreated);

        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message.ToString());

        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, [FromBody] UpdateCustomerRequest request)
    {
        try
        {

            var phone = PhoneNumbersExtensions.TryToGetFromPhoneNumber(request.PhoneNumber.ToString());
            var validateRider = CustomerValidationRules.UpdateValidation(request);

            if (!validateRider.IsValid)
            {
                var currentRiderExceptions = validateRider.Errors.Select(f => $"{f}").ToList();
                throw new BadHttpRequestException(string.Join(",", currentRiderExceptions));
            }

            var customerUpdated = await _mediator.Send(new UpdateCustomerCommand(id, request.Firstname, request.Lastname,
                request.DateOfBirth, ulong.Parse(phone), new MailAddress(request.Email), request.BankAccountNumber));
            return Ok(customerUpdated);

        }
        catch (Exception e)
        {
            throw new BadHttpRequestException(e.Message.ToString());

        }
    }



    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid id)
    {

        await _mediator.Send(new DeleteCustomerCommand(id));
        return NoContent();

    }

}