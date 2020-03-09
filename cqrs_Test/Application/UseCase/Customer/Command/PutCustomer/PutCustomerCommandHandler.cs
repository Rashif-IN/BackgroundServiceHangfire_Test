using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Customer.Command.PutCustomer
{
    public class PutCustomerCommandHandler : IRequestHandler<PutCustomerCommand, PutCustomerCommandDto>
    {
        private readonly IContext konteks;

        public PutCustomerCommandHandler(IContext context)
        {
            konteks = context;
        }
        

        public async Task<PutCustomerCommandDto> Handle(PutCustomerCommand request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine($"Put Customer Data {request.Dataa.Attributes.id}"));
            var customers = konteks.Customer.Find(request.Dataa.Attributes.id);
            customers.full_name = request.Dataa.Attributes.full_name;
            customers.username = request.Dataa.Attributes.username;
            customers.birthdate = request.Dataa.Attributes.birthdate;
            customers.password = request.Dataa.Attributes.password;
            customers.sex = request.Dataa.Attributes.sex;
            customers.email = request.Dataa.Attributes.email;
            customers.phone_number = request.Dataa.Attributes.phone_number;
            customers.updated_at = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds);

            await konteks.SaveChangesAsync(cancellationToken);

            return new PutCustomerCommandDto
            {
                Status = true,
                Message = $"Customer {request.Dataa.Attributes.id} successfully putted",
            };
        }
    }
}
