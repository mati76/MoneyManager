using System;

namespace MoneyManager.Business.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public string AddUserName { get; set; }

        public string UpdUserName { get; set; }

        public DateTime AddDateTime { get; set; }

        public DateTime? UpdDateTime { get; set; }
    }
}