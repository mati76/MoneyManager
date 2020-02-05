using System;
using System.Text.RegularExpressions;

namespace MoneyManager.Integrations.CSV
{
	internal class FormatHelper : IFormatHelper
	{
		public T Convert<T>(string value, IFormatProvider cultureInfo)
		{
			return (T)System.Convert.ChangeType(value, typeof(T), cultureInfo);
		}

		public string StripCharacters(string value, char c)
		{
			return value.Replace(c.ToString(), string.Empty);
		}

		public string ExtractWithRegex(string value, Regex regex)
		{
			var matches = regex.Matches(value);
			if (matches.Count > 0)
			{
				value = string.Empty;
				foreach (var match in matches)
					value += match;
			}
			return value;
		}
	}
}
