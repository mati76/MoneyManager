
namespace MoneyManager.WebApi.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }

        //public string Color { get; set; }

        public int? ParentId { get; set; }
    }
}