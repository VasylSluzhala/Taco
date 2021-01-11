using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Taco.Core;
using Taco.Core.Entities;

namespace Taco.DAL.DataProvider
{
    public class JsonProvider : IDataProvider
    {
        private readonly ConfigurationFactory _factory;

        public JsonProvider(ConfigurationFactory factory)
        {
            _factory = factory;
        }

        public Task<List<Restaurant>> GetRestaurantList()
        {
            var path = Path.Combine(_factory.ContentRootPath, _factory.DataPath);
            var text = File.ReadAllText(path);
            var result = JsonConvert.DeserializeObject<List<Restaurant>>(text);
            return Task.FromResult(result);
        }
    }
}
