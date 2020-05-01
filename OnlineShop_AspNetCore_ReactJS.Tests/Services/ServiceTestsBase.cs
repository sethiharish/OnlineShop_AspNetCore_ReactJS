using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using System;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public abstract class ServiceTestsBase
    {
        protected readonly DbContextOptions<OnlineShopContext> options;

        public ServiceTestsBase()
        {
            // Arrange
            options = new DbContextOptionsBuilder<OnlineShopContext>()
                            .UseInMemoryDatabase($"InMemoryDatabase_{Guid.NewGuid()}")
                            .Options;
        }
    }
}
