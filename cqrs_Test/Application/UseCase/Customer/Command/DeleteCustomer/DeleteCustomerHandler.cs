using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Customer.Command.DeleteCustomer
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerCommandDto>
    {
        private readonly IContext konteks;

        public DeleteCustomerHandler(IContext context)
        {
            konteks = context;
        }
        

        public async Task<DeleteCustomerCommandDto> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
           
            var data = await konteks.Customer.FindAsync(request.Id);

            if (data == null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Customer not found"));
                return null;
            }
            else
            {
                konteks.Customer.Remove(data);
                await konteks.SaveChangesAsync(cancellationToken);
                BackgroundJob.Enqueue(() => Console.WriteLine($"Delete Customer Data{request.Id}"));
                return new DeleteCustomerCommandDto
                {
                    Status = true,
                    Message = "Customer sucessfully added"
                };
            }
            //throw new NotImplementedException();
        }
    }
}
