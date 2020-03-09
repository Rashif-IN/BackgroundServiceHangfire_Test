using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Merchant.Command.PutMerchant
{
    public class PutMerchantCommandHandler : IRequestHandler<PutMerchantCommand, PutMerchantCommandDto>
    {
        private readonly IContext konteks;

        public PutMerchantCommandHandler(IContext context)
        {
            konteks = context;
        }
        

        public async Task<PutMerchantCommandDto> Handle(PutMerchantCommand request, CancellationToken cancellationToken)
        {
            var mer = konteks.merhcants.Find(request.Dataa.Attributes.id);
            mer.name = request.Dataa.Attributes.name;
            mer.image = request.Dataa.Attributes.image;
            mer.address = request.Dataa.Attributes.address;
            mer.rating = request.Dataa.Attributes.rating;
            mer.updated_at = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds);


            BackgroundJob.Enqueue(() => Console.WriteLine("Merchant successfully putted"));
            await konteks.SaveChangesAsync(cancellationToken);

            return new PutMerchantCommandDto
            {
                Status = true,
                Message = "Merchant successfully putted"
            };
        }
    }
}
