using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyManager.WebApi.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public DateTime AddDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public string AddUserName { get; set; }

        public string UpdateUserName { get; set; }
    }
}