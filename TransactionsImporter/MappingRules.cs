using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TransactionsImporter.Model;

namespace TransactionsImporter
{
	public class MappingRules : IEnumerable<MappingRule>
	{
        public IEnumerator<MappingRule> GetEnumerator()
        {
            var foodDescriptions = new string[]
            {
                "1-Minute", "WARZYWNIAK", "KONKOL", "BIEDRONKA", "STOKROTKA", "AUCHAN", "CUKIERNIA", "LIDL", "Delikatesy", "ZABKA", "NIEDZWIEDZ", "MiM s.c.", "NES-PRO", "DIETY OD BROKU",
            };
            var healthAndbeautyCareDescriptions = new string[]
            {
                "SUPER-PHARM", "DROGERIA", "Rossmann", "APTEKA"
            };
            var agdDescriptions = new string[]
            {
                "HOME AND YOU", "PEPCO", "Leroy Merlin", "EURO-NET", "MEDIAEXPERT", "IKEA"
            };
            var fuelDescriptions = new string[]
            {
                "LOTOS", "BP-", "ORLEN"
            };
            var carDescriptions = new string[]
            {
                "NORAUTO",
            };
            var restaurantHoldDescriptions = new string[]
            {
                "Ludovisko", "CESKY FILM", "BROWAR"
            };
            var houseHoldDescriptions = new string[]
            {
                "ENERGA-OBR", "VEIDIK", "G I ZIELENI W GDA", "SAUR NEPTUN", "PODSTAWOWA NR 85", "VECRTA", "PGNIG"
            };

            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory?.Contains(" i chemia domowa") == true || foodDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.Food
            };
            yield return new MappingRule
            {
                Predicate = t => (t.Description)?.ToLower().Contains("cinkciarz.pl") == true,
                Category = Category.Loans
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Zdrowie i uroda" || healthAndbeautyCareDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.HealthAndBeauty
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Paliwo" || fuelDescriptions.Any(d => (t.Description + (t.Description))?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.Fuel
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory?.Contains("Akcesoria i wyposa") == true || agdDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.AgdRtv
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory?.Contains("Serwis i cz") == true || carDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.Car
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory?.Contains("Jedzenie poza domem") == true ||
                    t.OriginalCategory?.Contains("Wyjścia i wydarzenia") == true
                    || restaurantHoldDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.Restaurants
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Ubezpieczenia",
                Category = Category.Insurance
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Przejazdy",
                Category = Category.Transportation
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Sport i hobby",
                Category = Category.Sport
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "TV, internet, telefon"  || t.OriginalCategory == "opłaty i odsetki"
                    || houseHoldDescriptions.Any(d => (t.Description)?.ToLower().Contains(d.ToLower()) == true),
                Category = Category.HouseHoldBills
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Odzież i obuwie" || t.OriginalCategory == "Prezenty i wsparcie",
                Category = Category.Clothes
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Art. dziecięce i zabawki",
                Category = Category.Toys
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Parking i opłaty",
                Category = Category.Parking
            };
            yield return new MappingRule
            {
                Predicate = t => t.OriginalCategory == "Podróże i wyjazdy",
                Category = Category.Travel
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
