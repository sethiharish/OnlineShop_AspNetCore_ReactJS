using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class IterationService : IIterationService
    {
        private readonly OnlineShopContext context;

        public IterationService(OnlineShopContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Iteration>> GetIterationsAsync()
        {
            return await context.Iteration.ToListAsync();
        }
    }
}
