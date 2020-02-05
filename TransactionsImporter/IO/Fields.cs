using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TransactionsImporter.Model;

namespace TransactionsImporter.IO
{
	public class Fields : IEnumerable<Field>
	{

		private IFormatProvider ci => CultureInfo.InvariantCulture;
		public IEnumerator<Field> GetEnumerator()
		{
			yield return new Field
			{
				Name = "Amount",
				Index = 0,
				WriteTo = (t, s) => t.Amount = decimal.Parse(s, ci),
				ReadFrom = t => t.Amount.ToString(ci)
			};
			yield return new Field
			{
				Name = "Date",
				Index = 1,
				WriteTo = (t, s) => t.Date = DateTime.Parse(s, ci),
				ReadFrom = t => t.Date.ToString(ci)
			};
			yield return new Field
			{
				Name = "OriginalCategory",
				Index = 2,
				WriteTo = (t, s) => t.OriginalCategory = s,
				ReadFrom = t => t.OriginalCategory
			};
			yield return new Field
			{
				Name = "Category",
				Index = 3,
				WriteTo = (t, s) =>
				{
					t.Category = typeof(Category)
					 .GetFields(BindingFlags.Public | BindingFlags.Static)
					 .Where(f => f.FieldType == typeof(Category))
					 .Select(f => (Category)f.GetValue(null))
					 .FirstOrDefault(c => c.Name == s);
				},
				ReadFrom = t => t.Category?.Name
			};
			yield return new Field
			{
				Name = "Description",
				Index = 4,
				WriteTo = (t, s) => t.Description = s,
				ReadFrom = t => t.Description
			};
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
