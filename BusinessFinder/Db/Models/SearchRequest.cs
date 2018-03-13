using System;

namespace BusinessFinder.Db.Models
{
    public class SearchRequest
    {
        public int Id { get; set; }

        public string Search { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
