using BusinessFinder.Infrastructure;

namespace BusinessFinder.Db.Models
{
    public class Category
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public CategoryLevel CategoryLevel { get; set; }
    }
}
