
using System.Collections.Generic;

namespace MoneyManager.DataAccess.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int? ParentId { get; set; }

        public Category Parent { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
