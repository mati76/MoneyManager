using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyManager.DataAccess.Models;

namespace MoneyManager.DataAccess.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<MoneyManagerContext>
    {
        private const string user = "default";

        protected override void Seed(MoneyManagerContext context)
        {
            var categories = defaultCategories;
            categories.ForEach(c =>
            {
                c.AddDateTime = DateTime.Now;
                c.AddUserName = user;
                ((List<Category>)c.Categories).ForEach(cc =>
                {
                    cc.AddUserName = user;
                    cc.AddDateTime = DateTime.Now;
                });
            });
            context.Category.AddRange(categories);

            base.Seed(context);
        }

        protected List<Category> defaultCategories
        {
            get
            {
                return new List<Category>
                {
                    new Category
                    {
                        Name = "Opłaty",
                        Categories = new List<Category>
                        {
                            new Category { Name = "Rachunki" },
                            new Category { Name = "Edukacja / rozwój" },
                            new Category { Name = "Podatki" },
                            new Category { Name = "Raty kredytów" },
                            new Category { Name = "Szkoła / przedszkole" },
                            new Category { Name = "Ubezpieczenia" },
                        }
                    },
                    new Category
                    {
                        Name = "Samochód",
                        Categories = new List<Category>
                        {
                            new Category { Name = "Paliwo" },
                            new Category { Name = "Przeglądy" },
                            new Category { Name = "Naprawy" },
                            new Category { Name = "Mycie / pielęgnacja" },
                            new Category { Name = "Ubezpieczenia komunikacyjne" }
                        }
                    },
                    new Category
                    {
                        Name = "Wydatki codzienne",
                        Categories = new List<Category>
                        {
                            new Category { Name = "Artykuły spożywcze" },
                            new Category { Name = "Chemia / kosmetyki" },
                            new Category { Name = "Medycyna" }
                        }
                    },
                    new Category
                    {
                        Name = "Wydatki ekstra",
                        Categories = new List<Category>
                        {
                            new Category { Name = "Ubrania" },
                            new Category { Name = "Jedzenie na mieście" },
                            new Category { Name = "Dom" },
                            new Category { Name = "Ogród" },
                            new Category { Name = "Wakacje" },
                            new Category { Name = "AGD / RTV / IT" },
                            new Category { Name = "Rozrywka" },
                            new Category { Name = "Zabawki" }
                        }
                    },
                    new Category
                    {
                        Name = "Uroda i zdrowie",
                        Categories = new List<Category>
                        {
                            new Category { Name = "Kosmetyczka" },
                            new Category { Name = "Fryzjer" },
                            new Category { Name = "Sport" }
                        }
                    }
                };
            }
        }
    }
}
