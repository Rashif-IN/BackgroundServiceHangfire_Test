using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.CustomerPaymentCard.Command.PostCustomerPaymentCard
{
    public class PostCustomerPaymentCardCommandHandler : IRequestHandler<PostCustomerPaymentCardCommand, PostCustomerPaymentCardCommandDto>
    {
        private readonly IContext konteks;

        public PostCustomerPaymentCardCommandHandler(IContext context)
        {
            konteks = context;
        }

       

        public async Task<PostCustomerPaymentCardCommandDto> Handle(PostCustomerPaymentCardCommand request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Customer payment card posted"));
            var cpc = new Domain.Entities.CustomerPaymentCards
            {
                id = request.Dataa.Attributes.id,
                customer_id = request.Dataa.Attributes.customer_id,
                name_on_card = request.Dataa.Attributes.name_on_card,
                exp_month = request.Dataa.Attributes.exp_month,
                exp_year = request.Dataa.Attributes.exp_year,
                postal_code = request.Dataa.Attributes.postal_code,
                credit_card_number = request.Dataa.Attributes.credit_card_number
            };

            konteks.CPC.Add(cpc);
            await konteks.SaveChangesAsync(cancellationToken);

            return new PostCustomerPaymentCardCommandDto
            {
                Status = true,
                Message = "Customer Payment Card successfully posted"
            };
        }
    }
}
