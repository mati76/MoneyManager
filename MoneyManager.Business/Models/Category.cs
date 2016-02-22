
using System.Collections.Generic;

namespace MoneyManager.Business.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int? ParentId { get; set; }

        public Category Parent { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
