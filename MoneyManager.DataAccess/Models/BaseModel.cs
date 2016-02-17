using System;

namespace MoneyManager.DataAccess.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime AddDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public string AddUserName { get; set; }

        public string UpdateUserName { get; set; }
    }
}
