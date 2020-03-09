
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqrs_Test.Application.UseCase.Product.Command.DeleteProduct;
using cqrs_Test.Application.UseCase.Product.Command.PostProduct;
using cqrs_Test.Application.UseCase.Product.Command.PutProduct;
using cqrs_Test.Application.UseCase.Product.Queries.GetProduct;
using cqrs_Test.Application.UseCase.Product.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cqrs_Test.Presenter.Controller
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private IMediator meciater;

        public ProductController(IMediator mediatr)
        {
            meciater = mediatr;
        }


        [HttpGet]
        public async Task<ActionResult<GetProductsDto>> Get()
        {
            var result = new GetProductsQuery();
            return Ok(await meciater.Send(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = new GetProductQuery(ID);
            var wait = await meciater.Send(result);
            return wait != null ? (IActionResult)Ok(wait) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostProductCommand data)
        {
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int ID, PutProductCommand data)
        {
            data.Dataa.Attributes.id = ID;
            var result = await meciater.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            var command = new DeleteProductCommand(ID);
            var result = await meciater.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }

    }
}
