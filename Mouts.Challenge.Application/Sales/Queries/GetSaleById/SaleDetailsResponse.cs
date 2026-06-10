using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Queries.GetSaleById
{
    public sealed record SaleDetailsResponse(
     int Id,
     string SaleNumber,
     DateTime SaleDate,
     string CustomerName,
     string BranchName,
     decimal TotalAmount,
     bool IsCancelled,
     List<SaleItemResponse> Items);

    public sealed record SaleItemResponse(
    int Id,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal DiscountAmount,
    decimal TotalAmount);
}
