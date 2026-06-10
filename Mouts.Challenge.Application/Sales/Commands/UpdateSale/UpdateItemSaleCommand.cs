using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.UpdateSale
{
    public sealed record UpdateSaleItemCommand(
      int ProductId,
      string ProductName,
      int Quantity,
      decimal UnitPrice);
}
