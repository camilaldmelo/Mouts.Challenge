using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Domain.Entities
{
    public class SaleItem
    {

        private SaleItem()
        {
        }

        public SaleItem(
            int productId,
            string productName,
            int quantity,
            decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;

            Quantity = quantity;
            UnitPrice = unitPrice;

            Validate();

            DiscountPercentage = CalculateDiscountPercentage();
            DiscountAmount = CalculateDiscountAmount();
        }

        public int Id { get; private set; }

        public int ProductId { get; private set; }

        public string ProductName { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal DiscountPercentage { get; private set; }

        public decimal DiscountAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        public decimal GrossAmount => Quantity * UnitPrice;

        public decimal TotalAmount => GrossAmount - DiscountAmount;

        public void Cancel()
        {
            if (IsCancelled)
                return;

            IsCancelled = true;
        }

        private void Validate()
        {
            if (ProductId <= 0)
                throw new InvalidOperationException("Invalid product.");

            if (Quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero.");

            if (UnitPrice <= 0)
                throw new InvalidOperationException("Unit price must be greater than zero.");

            if (Quantity > 20)
                throw new InvalidOperationException(
                    "It is not possible to sell more than 20 identical items.");
        }

        private decimal CalculateDiscountPercentage()
        {
            // 10 a 20 itens = 20%
            if (Quantity >= 10 && Quantity <= 20)
                return 20m;

            // acima de 4 itens = 10%
            if (Quantity > 4)
                return 10m;

            // abaixo de 4 itens = sem desconto
            return 0m;
        }

        private decimal CalculateDiscountAmount()
        {
            return GrossAmount * (DiscountPercentage / 100);
        }
    }
}
