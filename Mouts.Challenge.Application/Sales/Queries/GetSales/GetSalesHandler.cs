using MediatR;
using Mouts.Challenge.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Queries.GetSales
{
    public sealed class GetSalesHandler
     : IRequestHandler<GetSalesQuery, List<SaleSummaryResponse>>
    {
        private readonly ISaleRepository _repository;

        public GetSalesHandler(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SaleSummaryResponse>> Handle(
            GetSalesQuery request,
            CancellationToken cancellationToken)
        {
            var sales = await _repository.GetAllAsync(cancellationToken);

            return sales.Select(sale =>
                new SaleSummaryResponse(
                    sale.Id,
                    sale.SaleNumber,
                    sale.SaleDate,
                    sale.CustomerName,
                    sale.BranchName,
                    sale.TotalAmount,
                    sale.IsCancelled))
                .ToList();
        }
    }
}
