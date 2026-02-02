using System;
using System.Collections.Generic;
using System.Text;
using Zaku.Domain.Entities;
using Zaku.Infrastructure.Data;

namespace Zaku.Infrastructure.Repositories
{
    internal class OrderRepository : Repository<Order>
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
