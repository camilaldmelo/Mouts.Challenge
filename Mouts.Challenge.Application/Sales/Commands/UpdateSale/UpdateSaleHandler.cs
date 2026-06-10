using MediatR;
using Mouts.Challenge.Application.Common.Interfaces;
using Mouts.Challenge.Application.Sales.Interfaces;
using Mouts.Challenge.Domain.Entities;
using Mouts.Challenge.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Sales.Commands.UpdateSale
{
    public sealed class UpdateSaleHandler
    : IRequestHandler<UpdateSaleCommand>
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _publisher;

        public UpdateSaleHandler(
            ISaleRepository repository,
            IUnitOfWork unitOfWork,
            IEventPublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task Handle(
            UpdateSaleCommand request,
            CancellationToken cancellationToken)
        {
            var sale = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException();

            var items = request.Items.Select(item =>
            new SaleItem(
            item.ProductId,
            item.ProductName,
            item.Quantity,
            item.UnitPrice));

            sale.Update(
                request.SaleDate,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName,
                items);

            _repository.Update(sale);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _repository.Update(sale);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            await _publisher.PublishAsync(
                new SaleModifiedEvent(sale.Id),
                cancellationToken);
        }
    }
}
