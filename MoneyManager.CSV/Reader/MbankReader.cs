using MoneyManager.Integrations.CSV.Model;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoneyManager.Integrations.CSV.Reader
{
	internal class MbankReader : IReaderImplmentation
	{
		protected readonly IFormatHelper formatHelper;
		protected IFormatProvider cultureInfo => new CultureInfo("pl-PL");

		public MbankReader() : this(new FormatHelper()) { }

		public MbankReader(IFormatHelper formatHelper)
		{
			this.formatHelper = formatHelper;
		}

		public Transaction ParseLine(string[] line)
		{
			return new Transaction
			{
				Date = formatHelper.Convert<DateTime>(line[0], cultureInfo),
				Description = formatHelper.ExtractWithRegex(line[1], new Regex(@"(\S+\s{1})+")),
				Category = formatHelper.StripCharacters(line[3], '"'),
				Amount = formatHelper.Convert<decimal>(formatHelper.ExtractWithRegex(line[4], new Regex(@"-*\d+[, .]\d+")), cultureInfo)
			};
		}
	}
}
