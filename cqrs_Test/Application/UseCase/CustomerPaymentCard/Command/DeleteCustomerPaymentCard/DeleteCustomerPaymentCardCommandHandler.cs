using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.DeleteCustomerPaymentCard
{
    public class DeleteCustomerPaymentCardCommandHandler : IRequestHandler<DeleteCustomerPaymentCardCommand, DeleteCustomerPaymentCardCommandDto>
    {
        private readonly IContext konteks;

        public DeleteCustomerPaymentCardCommandHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<DeleteCustomerPaymentCardCommandDto> Handle(DeleteCustomerPaymentCardCommand request, CancellationToken cancellationToken)
        {
            var data = await konteks.CPC.FindAsync(request.Id);
            if (data == null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Customer payment card not found"));
                return null;
            }
            else
            {
                konteks.CPC.Remove(data);
                await konteks.SaveChangesAsync(cancellationToken);
                BackgroundJob.Enqueue(() => Console.WriteLine($"Customer payment card {request.Id} deleted"));
                return new DeleteCustomerPaymentCardCommandDto
                {
                    Status = true,
                    Message = "Customer paymemt card sucessfully added"
                };
            }


        }
    }
}
