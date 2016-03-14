﻿using System;

namespace MoneyManager.WebApi.DTO
{
    public class Expense : BaseDTO
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; }

        public virtual Category Category { get; set; }
    }
}