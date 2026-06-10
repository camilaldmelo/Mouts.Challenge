using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mouts.Challenge.Application.Sales.Commands.CreateSale
{
    public sealed record CreateSaleCommand(
    string SaleNumber,
    DateTime SaleDate,
    int CustomerId,
    string CustomerName,
    int BranchId,
    string BranchName,
    List<CreateSaleItemCommand> Items
) : IRequest<CreateSaleResponse>;

}
