using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Product.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandDto>
    {
        private readonly IContext konteks;

        public DeleteProductCommandHandler(IContext context)
        {
            konteks = context;
        }
       

        public async Task<DeleteProductCommandDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var data = await konteks.merhcants.FindAsync(request.Id);
            if (data == null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Product not found"));
                return null;
            }
            else
            {
                konteks.merhcants.Remove(data);
                await konteks.SaveChangesAsync(cancellationToken);
                BackgroundJob.Enqueue(() => Console.WriteLine("Product successfully removed"));
                return new DeleteProductCommandDto
                {
                    Status = true,
                    Message = "Product sucessfully removed"
                };
            }
            //throw new NotImplementedException();
        }
    }
}
