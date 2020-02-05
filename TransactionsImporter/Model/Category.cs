using System;

namespace TransactionsImporter.Model
{
	public class Category
	{
		public static Category Food = new Category("Żywność i chemia domowa");
		public static Category HealthAndBeauty = new Category("Zdrowie i uroda");
		public static Category AgdRtv = new Category("AGD, RTV, wyposażenie domu");
		public static Category Fuel = new Category("Paliwo");
		public static Category Restaurants = new Category("Wyjścia i wydarzenia");
		public static Category HouseHoldBills = new Category("Opłaty");
		public static Category Clothes = new Category("Odzież i obuwie");
		public static Category GiftsAndCharity = new Category("Prezenty i wsparcie");
		public static Category Toys = new Category("Artykuły dziecięce i zabawki");
		public static Category Loans = new Category("Raty kredytów");
		public static Category Car = new Category("Samochód");
		public static Category Insurance = new Category("Ubezpieczenia");
		public static Category Parking = new Category("Parking i opłaty");
		public static Category Transportation = new Category("Przejazdy");
		public static Category Sport = new Category("Sport i hobby");
		public static Category Travel = new Category("Podróże i wyjazdy");
		public static Category Other = new Category("Inne");

		public string Name { get; set; }

		private Category(string name)
		{
			Name = name;
		}
	}
}
