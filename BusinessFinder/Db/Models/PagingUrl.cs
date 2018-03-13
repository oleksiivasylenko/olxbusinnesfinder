using System;

namespace BusinessFinder.Db.Models
{
    public class PagingUrl
    {
        public int Id { get; set; }

        public int SearchRequestId { get; set; }

        public string Url { get; set; } 

        public DateTime? HandledDate { get; set; }

        public SearchRequest SearchRequest { get; set; }
    }
}
