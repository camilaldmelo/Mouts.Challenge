using Mouts.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mouts.Challenge.Application.Common.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale, CancellationToken ct);
        Task<Sale?> GetByIdAsync(int id, CancellationToken ct);
        Task<IReadOnlyCollection<Sale>> GetAllAsync(CancellationToken ct);
        void Update(Sale sale);
    }
}
