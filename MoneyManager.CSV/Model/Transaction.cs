using System;

namespace MoneyManager.Integrations.CSV.Model
{
	public class Transaction
	{
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public string ExtraDescription { get; set; }
	}
}
