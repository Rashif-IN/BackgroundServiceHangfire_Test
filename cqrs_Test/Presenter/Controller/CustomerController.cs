
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs_Test.Application.UseCase.Customer.Command.DeleteCustomer;
using cqrs_Test.Application.UseCase.Customer.Command.PostCustomer;
using cqrs_Test.Application.UseCase.Customer.Command.PutCustomer;
using cqrs_Test.Application.UseCase.Customer.Queries.GetCustomer;
using cqrs_Test.Application.UseCase.Customer.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cqrs_Test.Presenter.Controller
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {

        private IMediator meciater;

        public CustomerController(IMediator mediatr)
        {
            meciater = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult<GetCustomersDto>> Get()
        {
            var result = new GetCustomersQuery();
            return Ok(await meciater.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = new GetCustomerQuery(ID);
            var wait = await meciater.Send(result);
            return wait != null ? (IActionResult)Ok(wait) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCustomerCommand data)
        {
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, PutCustomerCommand data)
        {
            data.Dataa.Attributes.id = ID;
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            var command = new DeleteCustomerCommand(ID);
            var result = await meciater.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
            //return Ok(result);
            //return null;
        }


    }
}