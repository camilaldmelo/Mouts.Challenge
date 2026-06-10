using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.UpdateSale
{
    public sealed record UpdateSaleCommand(
    int Id,
    DateTime SaleDate,
    int CustomerId,
    string CustomerName,
    int BranchId,
    string BranchName,
    List<UpdateSaleItemCommand> Items)
    : IRequest;
}
