using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionsImporter.Model
{
	public class EncodingInfo
	{
		private static EncodingInfo[] _encodings =  new EncodingInfo[]
		{
			new EncodingInfo
			{
				Encoding = Encoding.UTF8,
				Name = "UTF8"
			},
			new EncodingInfo
			{
				Encoding = Encoding.ASCII,
				Name = "ASCII"
			},
			new EncodingInfo
			{
				Encoding = CodePagesEncodingProvider.Instance.GetEncoding(1250),
				Name = "Windows 1250"
			}
		};

		public static IEnumerable<EncodingInfo> Encodings => _encodings;

		public Encoding Encoding { get; set; }
		public string Name { get; set; }
	}
}
