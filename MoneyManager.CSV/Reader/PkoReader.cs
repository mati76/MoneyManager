using MoneyManager.Integrations.CSV.Model;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoneyManager.Integrations.CSV.Reader
{
	internal class PkoReader : IReaderImplmentation
	{
		protected readonly IFormatHelper formatHelper;
		protected IFormatProvider cultureInfo => new CultureInfo("en-US");

		public PkoReader() : this(new FormatHelper()) { }
		public PkoReader(IFormatHelper formatHelper)
		{
			this.formatHelper = formatHelper;
		}

		public Transaction ParseLine(string[] line)
		{
			return new Transaction
			{
				Date = formatHelper.Convert<DateTime>(formatHelper.StripCharacters(line[0], '"'), cultureInfo),
				Description = formatHelper.StripCharacters(line[6], '"'),
				ExtraDescription = formatHelper.StripCharacters(line[7], '"'),
				Category = formatHelper.StripCharacters(line[2], '"'),
				Amount = formatHelper.Convert<decimal>(formatHelper.ExtractWithRegex(line[3], new Regex(@"-*\d+[,.]\d+")), cultureInfo)
			};
		}
	}
}
