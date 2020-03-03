using System;

namespace TransactionsImporter.Model
{
	public class Transaction : ModelBase
	{
		public Transaction()
		{
			Date = DateTime.Now;
			Id = Guid.NewGuid();
		}

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

		public Guid Id { get; private set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string OriginalCategory { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
		public bool IsExpense => Amount < 0;

		public override bool Equals(object transaction)
		{
			if (transaction is Transaction)
			{
				return Id == ((Transaction)transaction).Id;
			}
			return false;
		}

		public void Asign(Transaction t)
		{
			Id = t.Id;
			Amount = t.Amount;
			Date = t.Date;
			OriginalCategory = t.OriginalCategory;
			Category = Category;
			Description = Description;
		}
	}
}
