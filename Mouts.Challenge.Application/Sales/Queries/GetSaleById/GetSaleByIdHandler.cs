using MediatR;
using Mouts.Challenge.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Queries.GetSaleById
{
    public sealed class GetSaleByIdHandler
    : IRequestHandler<GetSaleByIdQuery, SaleDetailsResponse?>
    {
        private readonly ISaleRepository _repository;

        public GetSaleByIdHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaleDetailsResponse?> Handle(
            GetSaleByIdQuery request,
            CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (sale is null)
                return null;

            return new SaleDetailsResponse(
                sale.Id,
                sale.SaleNumber,
                sale.SaleDate,
                sale.CustomerName,
                sale.BranchName,
                sale.TotalAmount,
                sale.IsCancelled,
                sale.Items.Select(item =>
                    new SaleItemResponse(
                        item.Id,
                        item.ProductName,
                        item.Quantity,
                        item.UnitPrice,
                        item.DiscountAmount,
                        item.TotalAmount))
                    .ToList());
        }
    }
}
