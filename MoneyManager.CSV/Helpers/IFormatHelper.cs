using System;
using System.Text.RegularExpressions;

namespace MoneyManager.Integrations.CSV
{
	internal interface IFormatHelper
	{
		T Convert<T>(string value, IFormatProvider cultureInfo);
		string ExtractWithRegex(string value, Regex regex);
		string StripCharacters(string value, char character);
	}
}