using System;

namespace MoneyManager.Integrations.CSV.Model
{
	public class ReaderSettings
	{
		public ReaderSettings(char delimiter)
		{
			Delimiter = delimiter;
		}

		public int? FirstLine { get; set; }

		public Predicate<string> DetectFirstLine { get; set; }

		public char Delimiter { get; }
	}
}
