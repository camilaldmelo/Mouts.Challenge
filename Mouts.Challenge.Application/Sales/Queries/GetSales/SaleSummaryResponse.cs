using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Queries.GetSales
{
    public sealed record SaleSummaryResponse(
     int Id,
     string SaleNumber,
     DateTime saleDate,
     string CustomerName,
     string branchName,
     decimal TotalAmount,
     bool IsCancelled);
}
