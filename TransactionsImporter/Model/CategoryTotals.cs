namespace TransactionsImporter.Model
{
	public class CategoryTotals : ModelBase
	{
		private Category _category;
		public Category Category 
		{ 
			get => _category;
			set
			{
				_category = value;
				OnPropertyChanged("Category");
			}
		}

		private decimal _amount;
		public decimal Amount
		{
			get => _amount;
			set 
			{
				_amount = value;
				OnPropertyChanged("Amount");
			}
		}

		private int _count;
		public int Count
		{
			get => _count;
			set
			{
				_count = value;
				OnPropertyChanged("Count");
			}
		}
	}
}
