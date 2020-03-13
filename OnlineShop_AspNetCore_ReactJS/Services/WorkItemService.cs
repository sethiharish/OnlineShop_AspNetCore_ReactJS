using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly OnlineShopContext context;

        public WorkItemService(OnlineShopContext context)
        {
            this.context = context;
        }
        
        public async Task<IEnumerable<WorkItem>> GetWorkItemsAsync()
        {
            return await context.WorkItem.Include(i => i.Iteration).ToListAsync();
        }
    }
}
