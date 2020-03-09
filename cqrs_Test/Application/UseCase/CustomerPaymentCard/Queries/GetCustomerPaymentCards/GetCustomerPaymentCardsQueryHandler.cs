using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cqrs_Test.Application.UseCase.CustomerPaymentCard.Queries.GetCustomerPaymentCards
{
    public class GetCustomerPaymentCardsQueryHandler : IRequestHandler<GetCustomerPaymentCardsQuery, GetCustomerPaymentCardsDto>
    {
        private readonly IContext konteks;

        public GetCustomerPaymentCardsQueryHandler(IContext context)
        {
            konteks = context;
        }

        public async Task<GetCustomerPaymentCardsDto> Handle(GetCustomerPaymentCardsQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.CPC.ToListAsync();
            BackgroundJob.Enqueue(() => Console.WriteLine("Customer payment card retrieved"));
            return new GetCustomerPaymentCardsDto
            {
                Status = true,
                Message = "Customer payment card successfully retrieved",
                Data = result
            };

        }
    }
}
