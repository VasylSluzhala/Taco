using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taco.Core.Entities;

namespace Taco.DAL.DataProvider
{
    public class SqlServerProvider : IDataProvider
    {
        private readonly TacoDbContext _dbContext;

        public SqlServerProvider(TacoDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(_dbContext));
        }

        public async Task<List<Restaurant>> GetRestaurantList()
        {
            return await _dbContext.Restaurants
                .Include(x => x.Categories)
                .ThenInclude(x => x.MenuItems)
                .ToListAsync();
        }
    }
}
