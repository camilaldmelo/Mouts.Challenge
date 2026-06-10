using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.CreateSale
{
    public sealed class CreateSaleItemCommandValidator
       : AbstractValidator<CreateSaleItemCommand>
    {
        public CreateSaleItemCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 20);

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0);
        }
    }
}
