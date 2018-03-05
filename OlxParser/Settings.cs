using System;
using System.Collections.Generic;
using System.Linq;

namespace OlxParser
{
    public class Settings
    {
        public int LastPage { get; set; }

        public List<string> Links { get; set; }

        public List<string> HandledLinks { get; set; }

        public List<string> OrderLinks { get; set; }
        public List<string> HandledOrderLinks { get; set; }
        public DateTime Date { get; set; }

        public List<UrlCounter> UrlWithCounts { get; set; }

        public Settings()
        {
            Links = new List<string>();
            HandledLinks = new List<string>();
            OrderLinks = new List<string>();
            HandledOrderLinks = new List<string>();
            UrlWithCounts = new List<UrlCounter>();
        }

        public List<string> GetNotHandledLinks()
        {
            var links = Links.Where(l => !HandledLinks.Contains(l)).ToList();
            return links;
        }

        public List<string> GetNotHandledOrderLinks()
        {
            var links = OrderLinks.Where(l => !HandledOrderLinks.Contains(l)).ToList();
            return links;
        }
    }

    public class UrlCounter
    {
        public int Count { get; set; }

        public string Url { get; set; }
    }

}
