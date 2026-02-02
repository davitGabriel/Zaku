using System;
using System.Collections.Generic;
using System.Text;
using Zaku.Infrastructure.Data;
using Zaku.Domain.Interfaces;

namespace Zaku.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
