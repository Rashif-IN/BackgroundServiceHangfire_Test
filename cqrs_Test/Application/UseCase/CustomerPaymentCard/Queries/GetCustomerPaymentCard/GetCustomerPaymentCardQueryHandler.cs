using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cqrs_Test.Application.UseCase.CustomerPaymentCard.Queries.GetCustomerPaymentCard
{
    public class GetCustomerPaymentQueryHandler : IRequestHandler<GetCustomerPaymentCardQuery, GetCustomerPaymentCardDto>
    {
        private readonly IContext konteks;

        public GetCustomerPaymentQueryHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<GetCustomerPaymentCardDto> Handle(GetCustomerPaymentCardQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.CPC.FirstOrDefaultAsync(e => e.id == request.Id);

            if (result != null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine($"Customer payment card {request.Id} retrieved"));
                return new GetCustomerPaymentCardDto
                {
                    Status = true,
                    Message = "Customer payment card successfully retrieved",
                    Data = result
                };
            }
            else
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Customer payment card not found"));
                return null;
            }
            

        }
    }
}
