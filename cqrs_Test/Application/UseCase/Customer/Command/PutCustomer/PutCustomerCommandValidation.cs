using System;

using FluentValidation;
using Hangfire;

namespace cqrs_Test.Application.UseCase.Customer.Command.PutCustomer
{
    public class PutCustomerCommandValidation : AbstractValidator<PutCustomerCommand>
    {
        public PutCustomerCommandValidation()
        {
            
            RuleFor(x => x.Dataa.Attributes.username).NotEmpty().WithMessage("username can't be empty");
            RuleFor(x => x.Dataa.Attributes.username).MaximumLength(20).WithMessage("max username lenght is 20");
            RuleFor(x => x.Dataa.Attributes.email).NotEmpty().WithMessage("email can't be empty");
            RuleFor(x => x.Dataa.Attributes.email).EmailAddress().WithMessage("max username lenght is 20");
            //RuleFor(x => x.Dataa.Attributes.gender).IsInEnum().WithMessage("gender is one of male or female");
            RuleFor(x => x.Dataa.Attributes.sex).Must(y => y == "female" || y == "male").WithMessage("gender is one of male or female");
            RuleFor(x => x.Dataa.Attributes.sex).NotEmpty().WithMessage("gender can't be empty");
            RuleFor(x => x.Dataa.Attributes.birthdate).NotEmpty().WithMessage("birhdate can't be empty");
            RuleFor(x => DateTime.Now.Year - x.Dataa.Attributes.birthdate.Year).GreaterThan(18).WithMessage("age must be greater than 18");
        }
    }
}
