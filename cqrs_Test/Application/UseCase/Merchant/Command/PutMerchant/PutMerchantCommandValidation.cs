using System;
using FluentValidation;

namespace cqrs_Test.Application.UseCase.Merchant.Command.PutMerchant
{
    public class PutMerchantCommandValidation : AbstractValidator<PutMerchantCommand>
    {
        public PutMerchantCommandValidation()
        {
            RuleFor(x => x.Dataa.Attributes.name).NotEmpty().WithMessage("name can't be empty");
            RuleFor(x => x.Dataa.Attributes.address).NotEmpty().WithMessage("address can't be empty");
            RuleFor(x => x.Dataa.Attributes.rating).ExclusiveBetween(1, 5).WithMessage("address can't be empty");
        }
    }
}
