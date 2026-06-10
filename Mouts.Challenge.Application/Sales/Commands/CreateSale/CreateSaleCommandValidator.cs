using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.SaleDate)
                .NotEmpty();

            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.BranchId)
                .GreaterThan(0);

            RuleFor(x => x.BranchName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("The sale must contain at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateSaleItemCommandValidator());
        }
    }
}
