using MoneyManager.Api.Services;
using MoneyManager.Api.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Linq;
using MoneyManager.Api.Enums;

namespace MoneyManager.Api.Controllers
{
    [Route("api/expense")]
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase, IExpenseController
    {
        private IExpenseService _expenseService;
        private ICategoryService _categoryService;

        public ExpenseController(IExpenseService expenseService, ICategoryService categoryService)
        {
            if(expenseService == null)
            {
                throw new ArgumentNullException(nameof(expenseService));
            }
            _expenseService = expenseService;
            _categoryService = categoryService;
        }

        public async Task<TransactionCollection> Get([FromQuery]SearchCriteria criteria)
        {
            return await _expenseService.GetExpenses(criteria);
        }

        [Route("{id:int}")]
        public Task<Transaction> GetById(int id)
        {
            return _expenseService.GetExpense(id);
        }

        [Route("{date:datetime}")]
        public Task<IEnumerable<Transaction>> GetByDate(DateTime date)
        {
            return _expenseService.GetExpenses(date);
        }

        [Route("totals/{date:datetime}")]
        public Task<TransactionTotals> GetTotals(DateTime date)
        {
            return _expenseService.GetExpenseTotals(date);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category")]
        public Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo);
        }

        [Route("{dateFrom:datetime}/{dateTo:datetime}/category/{categoryId:int}")]
        public Task<IEnumerable<CategoryTotal>> GetCategoryTotals(DateTime dateFrom, DateTime dateTo, int categoryId)
        {
            return _expenseService.GetCategoryTotals(dateFrom, dateTo, categoryId);
        }

        [Route("{year:int:min(1900)}/{month:int:range(1,12)}")]
        public async Task<IEnumerable<Transaction>> GetByDate(int year, int month)
        {
            return await _expenseService.GetExpenses(year, month);
        }

        [Route("upload/transactions/linked")]
        [AllowAnonymous]
        public async Task<IEnumerable<Transaction>> UploadLinked([FromQuery]string filename, [FromQuery] string type, [FromQuery] string category)
        {
            var list = new List<Transaction>();
            var transactions = await Upload(filename, type);
            var categories = await _categoryService.GetCategories(CategoryTypeEnum.Expense);
            var rules = GetMappingRules().ToList();

            foreach (var t in transactions)
            {
                var ruleMet = rules.FirstOrDefault(r => r.Predicate(t));
                if (t.Amount < 0 && ruleMet != null)
                {
                    list.Add(new Transaction()
                    {
                        Amount = t.Amount,
                        CategoryId = ruleMet.CategoryId,
                        CategoryName = categories.SelectMany(c => c.Categories).FirstOrDefault(cc => cc.Id == ruleMet.CategoryId)?.Name,
                        Comment = $"{t.Description} {t.ExtraDescription}",
                        Date = t.Date
                    });
                }
            }

            if (!string.IsNullOrEmpty(category))
                return list.Where(t => t.CategoryName == category);
            return list;
        }

        [Route("upload/transactions/unlinked")]
        [AllowAnonymous]
        public async Task<IEnumerable<Transaction>> UploadUnLinked([FromQuery]string filename, [FromQuery] string type)
        {
            var list = new List<Transaction>();
            var transactions = await Upload(filename, type);
            var categories = await _categoryService.GetCategories(CategoryTypeEnum.Expense);
            var rules = GetMappingRules().ToList();

            foreach (var t in transactions)
            {
                var ruleMet = rules.FirstOrDefault(r => r.Predicate(t));
                if (t.Amount < 0 && ruleMet == null)
                {
                    list.Add(new Transaction()
                    {
                        Amount = t.Amount,
                        Comment = $"{t.Description} {t.ExtraDescription}",
                        Date = t.Date,
                        CategoryName = t.Category
                    });
                }
            }

            return list;
        }

        [Route("upload/transactions/totals")]
        [AllowAnonymous]
        public async Task<IEnumerable<CategoryTotal>> UploadUTotals([FromQuery]string filename, [FromQuery] string type)
        {
            var list = new List<Transaction>();
            var transactions = await Upload(filename, type);
            var categories = await _categoryService.GetCategories(CategoryTypeEnum.Expense);
            var rules = GetMappingRules().ToList();

            foreach (var t in transactions)
            {
                var ruleMet = rules.FirstOrDefault(r => r.Predicate(t));
                if (t.Amount < 0 && ruleMet != null)
                {
                    list.Add(new Transaction()
                    {
                        Amount = t.Amount,
                        CategoryId = ruleMet.CategoryId,
                        CategoryName = categories.SelectMany(c => c.Categories).FirstOrDefault(cc => cc.Id == ruleMet.CategoryId)?.Name,
                        Comment = $"{t.Description} {t.ExtraDescription}",
                        Date = t.Date
                    });
                }
            }
            return list.GroupBy(t => t.CategoryName, (key, g) 
                => new CategoryTotal 
                {
                    CategoryName = key,
                    TotalAmount = g.Sum(t => t.Amount),
                    TransactionsCount = g.Count()
                });
        }

        private async Task<IEnumerable<Integrations.CSV.Model.Transaction>> Upload(string filename, string type)
        {
            var reader = new Integrations.CSV.CsvFileReader();
            var fileType = type == "mbank" ? Integrations.CSV.Model.FileType.MbankCSV : Integrations.CSV.Model.FileType.PkoCSV;

            return await reader.ReadFile(filename, fileType);
        }

        public Task Post(Transaction expense)
        {
            return _expenseService.SaveExpense(expense);
        }

        [Route("{id:int}")]
        public Task Delete(int id)
        {
            return _expenseService.DeleteExpense(id);
        }

        private static IEnumerable<MappingRule> GetMappingRules()
        {
            var foodDescriptions = new string[]
            {
                "1-Minute", "WARZYWNIAK", "KONKOL", "BIEDRONKA", "STOKROTKA", "AUCHAN", "CUKIERNIA", "LIDL", "Delikatesy", "APTEKA", "ZABKA", "NIEDZWIEDZ", "MiM s.c.", "NES-PRO", "DIETY OD BROKU", 
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
                "ENERGA-OBR", "VEIDIK", "ZARZ\ufffdD DR\ufffdG I ZIELENI W GDA\ufffdSKU", "SAUR NEPTUN", "PODSTAWOWA NR 85", "VECRTA", "PGNIG"
            };


            yield return new MappingRule
            {
                Predicate = t => t.Category?.Contains(" i chemia domowa") == true || foodDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 15
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category == "TV, internet, telefon" || (t.Description + t.ExtraDescription)?.ToLower().Contains("cinkciarz.pl") == true,
                CategoryId = 5
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category == "Zdrowie i uroda" || healthAndbeautyCareDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 16
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category == "Paliwo" || fuelDescriptions.Any(d => (t.Description + (t.Description + t.ExtraDescription))?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 9
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category?.Contains("Akcesoria i wyposa") == true || agdDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 21
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category != null && t.Category.Contains("Serwis i cz") == true || carDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 10
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category != null && t.Category.Contains("Jedzenie poza domem") == true || restaurantHoldDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 20
            };
            yield return new MappingRule
            {
                Predicate = t => houseHoldDescriptions.Any(d => (t.Description + t.ExtraDescription)?.ToLower().Contains(d.ToLower()) == true),
                CategoryId = 2
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category == "Odzie\ufffd i obuwie" || t.Category == "Prezenty i wsparcie",
                CategoryId = 19
            };
            yield return new MappingRule
            {
                Predicate = t => t.Category == "Art. dzieci\ufffdce i zabawki",
                CategoryId = 26
            };
        }

        private class MappingRule
        {
            public Predicate<Integrations.CSV.Model.Transaction> Predicate { get; set; }
            public int CategoryId { get; set; }
        }
    }
}
