using System.Collections.Generic;
using System.Threading.Tasks;
using Taco.Core.Entities;

namespace Taco.DAL.DataProvider
{
    public interface IDataProvider
    {
        Task<List<Restaurant>> GetRestaurantList();
    }
}
