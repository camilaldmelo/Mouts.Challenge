using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.UpdateSale
{
    public sealed record UpdateSaleResponse(
        int Id,
        string SaleNumber,
        DateTime SaleDate,
        int CustomerId,
        string CustomerName,
        int BranchId,
        string BranchName,
        decimal TotalAmount,
        bool IsCancelled,
        IReadOnlyCollection<UpdateSaleItemResponse> Items
    );

    public sealed record UpdateSaleItemResponse(
        int Id,
        int ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal DiscountPercentage,
        decimal DiscountAmount,
        decimal TotalAmount,
        bool IsCancelled
    );
}
