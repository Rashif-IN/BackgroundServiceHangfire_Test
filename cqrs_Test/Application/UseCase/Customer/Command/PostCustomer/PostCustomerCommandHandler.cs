using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.Models.Query;
using cqrs_Test.Domain.Entities;
using Hangfire;
using MediatR;

namespace cqrs_Test.Application.UseCase.Customer.Command.PostCustomer
{
    public class PostCustomerCommandHandler : IRequestHandler<PostCustomerCommand, PostCustomerCommandDto>
    {
        private readonly IContext konteks;

        public PostCustomerCommandHandler(IContext context)
        {
            konteks = context;
        }
    

        public async Task<PostCustomerCommandDto> Handle(PostCustomerCommand request, CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Post Customer Data"));
            var customers = new Domain.Entities.Customers
            {
                full_name = request.Dataa.Attributes.full_name,
                username = request.Dataa.Attributes.username,
                birthdate = request.Dataa.Attributes.birthdate,
                password = request.Dataa.Attributes.password,
                sex = request.Dataa.Attributes.sex,
                email = request.Dataa.Attributes.email,
                phone_number = request.Dataa.Attributes.phone_number
            };
            if (request.Dataa.Attributes.sex == "male")
            {
                customers.gender = kelamin.male;
            }
            else if (request.Dataa.Attributes.sex == "female")
            {
                customers.gender = kelamin.female;
            }
            konteks.Customer.Add(customers);
            await konteks.SaveChangesAsync(cancellationToken);

            return new PostCustomerCommandDto
            {
                Status = true,
                Message = "Customer successfully posted",
            };
            //throw new NotImplementedException();
        }
    }
}
