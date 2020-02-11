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
				if (isSelected != value)
				{
					isSelected = value;
					OnPropertyChanged("IsSelected");
				}
			}
		}

		private bool isBeingSplitted;
		public bool IsBeingSplitted
		{
			get => isBeingSplitted;
			set
			{
				if (isBeingSplitted != value)
				{
					isBeingSplitted = value;
					OnPropertyChanged("IsBeingSplitted");
				}
			}
		}

		private bool isBeingEdited;
		public bool IsBeingEdited
		{
			get => isBeingEdited;
			set
			{
				if (isBeingEdited != value)
				{
					isBeingEdited = value;
					OnPropertyChanged("IsBeingEdited");
				}
			}
		}

		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string OriginalCategory { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
	}
}
