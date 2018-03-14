using System.Collections.Generic;

namespace BusinessFinder.Db.Models
{
    public class ProductOwner
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public List<Product> Products { get; set; }
    }
}
