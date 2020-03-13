using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public interface IIterationService
    {
        Task<IEnumerable<Iteration>> GetIterationsAsync();
    }
}
