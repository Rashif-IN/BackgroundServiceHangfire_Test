
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs_Test.Application.UseCase.Merchant.Command.DeleteMerchant;
using cqrs_Test.Application.UseCase.Merchant.Command.PostMerchant;
using cqrs_Test.Application.UseCase.Merchant.Command.PutMerchant;
using cqrs_Test.Application.UseCase.Merchant.Queries.GetMerchant;
using cqrs_Test.Application.UseCase.Merchant.Queries.GetMerchants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cqrs_Test.Presenter.Controller
{
    [ApiController]
    [Authorize]
    [Route("merchant")]
    public class MerchantController : ControllerBase
    {
        private IMediator meciater;

        public MerchantController(IMediator mediatr)
        {
            meciater = mediatr;
        }


        [HttpGet]
        public async Task<ActionResult<GetMerchantsDto>> Get()
        {
            var result = new GetMerchantsQuery();
            return Ok(await meciater.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = new GetMerchantQuery(ID);
            var wait = await meciater.Send(result);
            return wait != null ? (IActionResult)Ok(wait) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostMerchantCommand data)
        {
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, PutMerchantCommand data)
        {
            data.Dataa.Attributes.id = ID;
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            var command = new DeleteMerchantCommand(ID);
            var result = await meciater.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

    }
}
