using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Product.Command.PutProduct
{
    public class PutProductCommandHandler : IRequestHandler<PutProductCommand, PutProductCommandDto>
    {
        private readonly IContext konteks;

        public PutProductCommandHandler(IContext context)
        {
            konteks = context;
        }
        
        public async Task<PutProductCommandDto> Handle(PutProductCommand request, CancellationToken cancellationToken)
        {
            var pro = konteks.Product.Find(request.Dataa.Attributes.id);
            pro.merhcant_id = request.Dataa.Attributes.merhcant_id;
            pro.name = request.Dataa.Attributes.name;
            pro.price = request.Dataa.Attributes.price;
            pro.updated_at = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds);


            BackgroundJob.Enqueue(() => Console.WriteLine("Product successfully put"));
            await konteks.SaveChangesAsync(cancellationToken);

            return new PutProductCommandDto
            {
                Status = true,
                Message = "Product successfully putted"
            };
        }
    }
}
