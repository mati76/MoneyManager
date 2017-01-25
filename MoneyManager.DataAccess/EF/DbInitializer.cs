using System;
using System.Collections.Generic;
using System.Data.Entity;
using MoneyManager.DataAccess.Models;

namespace MoneyManager.DataAccess.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<MoneyManagerContext>
    {
        private const string user = "default";

        protected override void Seed(MoneyManagerContext context)
        {
            var expenseCategories = defaultCategories;
            expenseCategories.ForEach(c =>
            {
                c.AddDateTime = DateTime.Now;
                c.AddUserName = user;
                ((List<ExpenseCategory>)c.Categories).ForEach(cc =>
                {
                    cc.AddUserName = user;
                    cc.AddDateTime = DateTime.Now;
                });
            });
            context.ExpenseCategory.AddRange(expenseCategories);

            var incomeCategories = defaultIncomeCategories;
            incomeCategories.ForEach(c =>
            {
                c.AddDateTime = DateTime.Now;
                c.AddUserName = user;
            });

            context.IncomeCategory.AddRange(incomeCategories);
            base.Seed(context);
        }

        protected List<IncomeCategory> defaultIncomeCategories =>
            new List<IncomeCategory>
            {
                new IncomeCategory { Name = "Wynagrodzenie" },
                new IncomeCategory { Name = "Premia / Dodatki" },
                new IncomeCategory { Name = "Odsetki / Zyski Kapitałowe" },
                new IncomeCategory { Name = "Prezenty" },
                new IncomeCategory { Name = "Pozostałe" }
            };

        protected List<ExpenseCategory> defaultCategories =>
            new List<ExpenseCategory>
            {
                new ExpenseCategory
                {
                    Name = "Opłaty",
                    Categories = new List<ExpenseCategory>
                    {
                        new ExpenseCategory { Name = "Rachunki" },
                        new ExpenseCategory { Name = "Edukacja / rozwój" },
                        new ExpenseCategory { Name = "Podatki" },
                        new ExpenseCategory { Name = "Raty kredytów" },
                        new ExpenseCategory { Name = "Szkoła / przedszkole" },
                        new ExpenseCategory { Name = "Ubezpieczenia" },
                    }
                },
                new ExpenseCategory
                {
                    Name = "Samochód",
                    Categories = new List<ExpenseCategory>
                    {
                        new ExpenseCategory { Name = "Paliwo" },
                        new ExpenseCategory { Name = "Przeglądy" },
                        new ExpenseCategory { Name = "Naprawy" },
                        new ExpenseCategory { Name = "Mycie / pielęgnacja" },
                        new ExpenseCategory { Name = "Ubezpieczenia komunikacyjne" }
                    }
                },
                new ExpenseCategory
                {
                    Name = "Wydatki codzienne",
                    Categories = new List<ExpenseCategory>
                    {
                        new ExpenseCategory { Name = "Artykuły spożywcze" },
                        new ExpenseCategory { Name = "Chemia / kosmetyki" },
                        new ExpenseCategory { Name = "Medycyna" }
                    }
                },
                new ExpenseCategory
                {
                    Name = "Wydatki ekstra",
                    Categories = new List<ExpenseCategory>
                    {
                        new ExpenseCategory { Name = "Ubrania" },
                        new ExpenseCategory { Name = "Jedzenie na mieście" },
                        new ExpenseCategory { Name = "Dom" },
                        new ExpenseCategory { Name = "Ogród" },
                        new ExpenseCategory { Name = "Wakacje" },
                        new ExpenseCategory { Name = "AGD / RTV / IT" },
                        new ExpenseCategory { Name = "Rozrywka" },
                        new ExpenseCategory { Name = "Zabawki" }
                    }
                },
                new ExpenseCategory
                {
                    Name = "Uroda i zdrowie",
                    Categories = new List<ExpenseCategory>
                    {
                        new ExpenseCategory { Name = "Kosmetyczka" },
                        new ExpenseCategory { Name = "Fryzjer" },
                        new ExpenseCategory { Name = "Sport" }
                    }
                }
            };
    }
}
