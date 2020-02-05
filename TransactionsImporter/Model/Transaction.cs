using System;

namespace TransactionsImporter.Model
{
	public class Transaction : ModelBase
	{
		private bool isSelected;
		public bool IsSelected
		{
			get => isSelected;
			set
			{
				isSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}

		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string OriginalCategory { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
	}
}
