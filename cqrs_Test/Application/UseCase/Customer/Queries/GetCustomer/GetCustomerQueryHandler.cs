using System;
using System.Threading;
using System.Threading.Tasks;
using cqrs_Test.Application.Interfaces;
using cqrs_Test.Application.UseCase.Customer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                return new GetCustomerDto
                {
                    Status = true,
                    Message = "Customer successfully retrieved",
                    Data = result
                };
            }
            else
            {
                return new GetCustomerDto
                {
                    Status = false,
                    Message = "Customer not found",
                    Data = result
                };
            }
            

        }
    }
}
