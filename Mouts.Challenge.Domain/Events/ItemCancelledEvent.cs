using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Domain.Events
{
    public sealed record ItemCancelledEvent(
    int SaleId,
    int ItemId);
}
