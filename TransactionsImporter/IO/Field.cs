using System;
using TransactionsImporter.Model;

namespace TransactionsImporter.IO
{
	public class Field
	{
		public string Name { get; set; }
		public int Index { get; set; }
		public Action<Transaction, string> WriteTo { get; set; }
		public Func<Transaction, string> ReadFrom { get; set; }
	}
}
