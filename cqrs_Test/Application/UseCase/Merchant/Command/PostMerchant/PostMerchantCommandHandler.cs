using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Merchant.Command.PostMerchant
{
    public class PostMerchantCommandHandler : IRequestHandler<PostMerchantCommand, PostMerchantCommandDto>
    {
        private readonly IContext konteks;

        public PostMerchantCommandHandler(IContext context)
        {
            konteks = context;
        }
       

        public async Task<PostMerchantCommandDto> Handle(PostMerchantCommand request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Merchant successfully posted"));
            var mer = new Domain.Entities.Merchants
            {
                id = request.Dataa.Attributes.id,
                name = request.Dataa.Attributes.name,
                image = request.Dataa.Attributes.image,
                address = request.Dataa.Attributes.address,
                rating = request.Dataa.Attributes.rating
            };

            konteks.merhcants.Add(mer);
            await konteks.SaveChangesAsync(cancellationToken);

            return new PostMerchantCommandDto
            {
                Status = true,
                Message = "Merchant successfully posted"
            };
        }
    }
}
