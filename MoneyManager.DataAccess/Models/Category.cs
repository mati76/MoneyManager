
namespace MoneyManager.DataAccess.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public int? ParentId { get; set; }
    }
}
