using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.UseCase.Customer.Models;
using Hangfire;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace cqrs_Test.Application.UseCase.Customer.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerDto>
    {
        private readonly IContext konteks;

        public GetCustomerQueryHandler(IContext context)
        {
            konteks = context;
        }
        public async Task<GetCustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {

            var result = await konteks.Customer.FirstOrDefaultAsync(e => e.id == request.Id);

            if(result!=null)
            {
                BackgroundJob.Enqueue(() => Console.WriteLine($"Get Customer Data {request.Id}"));
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("kucing211", "kucing211@outlook.com"));
                message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
                message.Subject = "How you doin'?";

                message.Body = new TextPart("plain")
                {
                    Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.mailtrap.io", 2525, false);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("ccdced0fc36ee1", "215162738c26d0");

                    client.Send(message);
                    client.Disconnect(true);
                }

                return new GetCustomerDto
                {
                    Status = true,
                    Message = "Customer successfully retrieved",
                    Data = result
                };
            }
            else
            {
                BackgroundJob.Enqueue(() => Console.WriteLine("Customer Not Found"));
                return null;
            }
            

        }
    }
}
