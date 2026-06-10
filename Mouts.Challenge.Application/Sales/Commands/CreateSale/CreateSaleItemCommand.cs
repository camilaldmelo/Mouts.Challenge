using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Mouts.Challenge.Application.Sales.Commands.CreateSale
{
    public sealed record CreateSaleItemCommand(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);
}
