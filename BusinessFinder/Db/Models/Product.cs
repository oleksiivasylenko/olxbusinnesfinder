using System;

namespace BusinessFinder.Db.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int? SearchRequestId { get; set; }

        public int ProductOwnerId { get; set; }

        public string Url { get; set; }

        public DateTime? HandledDate { get; set; }

        public int ViewsCount { get; set; }

        public string AuthorName { get; set; }

        public int PhoneNumber { get; set; }

        public SearchRequest SearchRequest { get; set; }

        public ProductOwner ProductOwner { get; set; }
    }
}
