using System.Collections.Generic;

namespace Taco.Core.Entities
{
    public class Restaurant : BaseEntity<int>
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Suburb { get; set; }

        public string LogoPath { get; set; }

        public int Rank { get; set; }

        public virtual List<Category> Categories { get; set; }
    }
}
