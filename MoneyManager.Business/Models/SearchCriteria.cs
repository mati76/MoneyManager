﻿using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Models
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Categories = new List<Category>();
        }

        public DateTime DateFrom{ get; set; }

        public DateTime DateTo { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public bool? SortAsc { get; set; }

        public string SortBy { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public int? Take { get; set; } = 50;

        public int? Skip { get; set; } = 0;
    }
}
