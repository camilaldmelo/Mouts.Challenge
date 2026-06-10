using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.CancelSale
{
    public sealed record CancelSaleCommand(
     int SaleId)
     : IRequest;
}
