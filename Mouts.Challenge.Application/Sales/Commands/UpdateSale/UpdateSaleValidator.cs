using FluentValidation;
using Mouts.Challenge.Application.Sales.Commands.CreateSale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.UpdateSale
{
    public sealed class UpdateSaleValidator
    : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0);

            RuleFor(x => x.BranchId)
                .GreaterThan(0);

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(x => x.ProductId)
                        .GreaterThan(0);

                    item.RuleFor(x => x.Quantity)
                        .InclusiveBetween(1, 20);

                    item.RuleFor(x => x.UnitPrice)
                        .GreaterThan(0);
                });
        }
    }
}
