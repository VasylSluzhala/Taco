using System.Collections.Generic;

namespace Taco.Core.DTO
{
    public class RestaurantDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Suburb { get; set; }

        public string LogoPath { get; set; }

        public int Rank { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
