using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mouts.Challenge.Domain.Entities
{
    public class Sale
    {
        private readonly List<SaleItem> _items = new();

        private Sale()
        {
        }

        public Sale(
            string saleNumber,
            DateTime saleDate,
            int customerId,
            string customerName,
            int branchId,
            string branchName)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;

            CustomerId = customerId;
            CustomerName = customerName;

            BranchId = branchId;
            BranchName = branchName;
        }

        public int Id { get; private set; }

        public string SaleNumber { get; private set; } = string.Empty;

        public DateTime SaleDate { get; private set; }

        public int CustomerId { get; private set; }

        public string CustomerName { get; private set; } = string.Empty;

        public int BranchId { get; private set; }

        public string BranchName { get; private set; } = string.Empty;

        public bool IsCancelled { get; private set; }

        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public decimal TotalAmount =>
            _items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);

        public void AddItem(
            int productId,
            string productName,
            int quantity,
            decimal unitPrice)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Cannot add items to a cancelled sale.");

            var item = new SaleItem(
                productId,
                productName,
                quantity,
                unitPrice);

            _items.Add(item);
        }

        public void RemoveItem(int itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);

            if (item is null)
                throw new InvalidOperationException("Item not found.");

            _items.Remove(item);
        }

        public void Cancel()
        {
            if (IsCancelled)
                return;

            IsCancelled = true;
        }

        public void CancelItem(int itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);

            if (item is null)
                throw new InvalidOperationException("Item not found.");

            item.Cancel();
        }

        public void Update(
          DateTime saleDate,
          int customerId,
          string customerName,
          int branchId,
          string branchName,
          IEnumerable<SaleItem> items)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Cancelled sales cannot be updated.");

            SaleDate = saleDate;
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;

            _items.Clear();

            foreach (var item in items)
                _items.Add(item);
        }

    }
}
