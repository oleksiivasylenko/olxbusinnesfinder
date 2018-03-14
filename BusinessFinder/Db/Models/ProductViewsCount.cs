using System;

namespace BusinessFinder.Db.Models
{
    public class ProductViewsCount
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ViewsCount { get; set; }

        public DateTime CheckedDate { get; set; }

        public Product Product { get; set; }
    }
}
