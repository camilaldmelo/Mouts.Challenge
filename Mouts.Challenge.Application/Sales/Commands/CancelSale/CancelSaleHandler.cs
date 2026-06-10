using MediatR;
using Mouts.Challenge.Application.Common.Interfaces;
using Mouts.Challenge.Application.Sales.Interfaces;
using Mouts.Challenge.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.CancelSale
{
    public sealed class CancelSaleHandler
      : IRequestHandler<CancelSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _publisher;

        public CancelSaleHandler(
            ISaleRepository repository,
            IUnitOfWork unitOfWork,
            IEventPublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task Handle(
            CancelSaleCommand request,
            CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(
                request.SaleId,
                cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException();

            sale.Cancel();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            await _publisher.PublishAsync(
                new SaleCancelledEvent(sale.Id),
                cancellationToken);
        }
    }
}
