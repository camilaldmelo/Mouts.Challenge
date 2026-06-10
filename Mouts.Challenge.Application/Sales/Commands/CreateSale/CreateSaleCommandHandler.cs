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

namespace Mouts.Challenge.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResponse>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;

        public CreateSaleCommandHandler(ISaleRepository saleRepository,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
        }

        public async Task<CreateSaleResponse> Handle(
            CreateSaleCommand request,
            CancellationToken cancellationToken)
        {
            var sale = new Sale(
                request.SaleNumber,
                request.SaleDate,
                request.CustomerId,
                request.CustomerName,
                request.BranchId,
                request.BranchName);

            foreach (var item in request.Items)
            {
                sale.AddItem(
                    item.ProductId,
                    item.ProductName,
                    item.Quantity,
                    item.UnitPrice);
            }

            await _saleRepository.AddAsync(sale, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _eventPublisher.PublishAsync(
                new SaleCreatedEvent(sale.Id),
                cancellationToken);
            return MapToResponse(sale);
        }

        private static CreateSaleResponse MapToResponse(Sale sale)
        {
            return new CreateSaleResponse(
                sale.Id,
                sale.SaleNumber,
                sale.SaleDate,
                sale.CustomerId,
                sale.CustomerName,
                sale.BranchId,
                sale.BranchName,
                sale.TotalAmount,
                sale.IsCancelled,
                sale.Items.Select(item =>
                    new CreateSaleItemResponse(
                        item.Id,
                        item.ProductId,
                        item.ProductName,
                        item.Quantity,
                        item.UnitPrice,
                        item.DiscountPercentage,
                        item.DiscountAmount,
                        item.TotalAmount,
                        item.IsCancelled))
                    .ToList());
        }
    }
}
