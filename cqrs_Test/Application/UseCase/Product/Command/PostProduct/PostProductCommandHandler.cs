using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Product.Command.PostProduct
{
    public class PostProductCommandHandler : IRequestHandler<PostProductCommand, PostProductCommandDto>
    {
       
        
        private readonly IContext konteks;
    

        public PostProductCommandHandler(IContext context)
        {
            konteks = context;
        }

        public async Task<PostProductCommandDto> Handle(PostProductCommand request, CancellationToken cancellationToken)
        {
            var pro = new Domain.Entities.Products
            {
                id = request.Dataa.Attributes.id,
                merhcant_id = request.Dataa.Attributes.merhcant_id,
                name = request.Dataa.Attributes.name,
                price = request.Dataa.Attributes.price,
                created_at = request.Dataa.Attributes.created_at,
                updated_at = request.Dataa.Attributes.updated_at
            };
            BackgroundJob.Enqueue(() => Console.WriteLine("Product successfully posted"));
            konteks.Product.Add(pro);
            await konteks.SaveChangesAsync(cancellationToken);

            return new PostProductCommandDto
            {
                Status = true,
                Message = "Product successfully posted"
            };
        }
    }
        
}


