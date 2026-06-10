using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mouts.Challenge.Application.Common.Interfaces;
using Mouts.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale, CancellationToken ct)
        {
            await _context.Sales.AddAsync(sale, ct);
        }

        public async Task<Sale?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Sales
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<IReadOnlyCollection<Sale>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Sales
                .Include(x => x.Items)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public void Update(Sale sale)
        {
            _context.Sales.Update(sale);
        }
    }
}