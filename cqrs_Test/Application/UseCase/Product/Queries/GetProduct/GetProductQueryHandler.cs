using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cqrs_Test.Application.UseCase.Product.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductDto>
    {
        private readonly IContext konteks;

        public GetProductQueryHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<GetProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.Product.FirstOrDefaultAsync(e => e.id == request.Id);

            if (result != null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Product successfully retrieved"));
                return new GetProductDto
                {
                    Status = true,
                    Message = "Product successfully retrieved",
                    Data = result
                };
            }
            else
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Product not found"));
                return null;
            }
            

        }
    }
}
