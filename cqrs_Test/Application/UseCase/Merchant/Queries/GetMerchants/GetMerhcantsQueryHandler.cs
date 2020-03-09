using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cqrs_Test.Application.UseCase.Merchant.Queries.GetMerchants
{
    public class GetMerhcantsQueryHandler : IRequestHandler<GetMerchantsQuery, GetMerchantsDto>
    {
        private readonly IContext konteks;

        public GetMerhcantsQueryHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<GetMerchantsDto> Handle(GetMerchantsQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.merhcants.ToListAsync();
            BackgroundJob.Enqueue(() => Console.WriteLine("Merchant successfully retrieved"));
            return new GetMerchantsDto
            {
                Status = true,
                Message = "Merchant successfully retrieved",
                Data = result
            };

        }
    }
}
