using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cqrs_Test.Application.UseCase.Merchant.Queries.GetMerchant
{
    public class GetMerchantQueryHandler : IRequestHandler<GetMerchantQuery, GetMerchantDto>
    {
        private readonly IContext konteks;

        public GetMerchantQueryHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<GetMerchantDto> Handle(GetMerchantQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.merhcants.FirstOrDefaultAsync(e => e.id == request.Id);

            if (result != null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Merchant successfully retrieved"));
                return new GetMerchantDto
                {
                    Status = true,
                    Message = "Merchant successfully retrieved",
                    Data = result
                };
            }
            else
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Merchant not found"));
                return null;
            }

        }
    }
}
