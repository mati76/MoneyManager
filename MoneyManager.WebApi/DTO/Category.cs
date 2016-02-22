using System.Collections.Generic;

namespace MoneyManager.WebApi.DTO
{
    public class Category : BaseDTO
    {
        public string Name { get; set; }

        //public string Color { get; set; }

        public int? ParentId { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}