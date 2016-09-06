using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MoneyManager.DataAccess.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<MoneyManagerContext>
    {
        private const string user = "default";

        protected override void Seed(MoneyManagerContext context)
        {
            context.Category.Add(defaultCategories.First());

            base.Seed(context);
        }

        protected IEnumerable<Models.Category> defaultCategories =>
            new List<Models.Category>
            {
                new Models.Category { Name = "Test", AddDateTime = DateTime.Now, AddUserName = user}
            };
    }
}
