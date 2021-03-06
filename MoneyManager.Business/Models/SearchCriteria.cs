﻿using System;
using System.Collections.Generic;

namespace MoneyManager.Business.Models
{
    public class SearchCriteria
    {
        public DateTime DateFrom{ get; set; }

        public DateTime DateTo { get; set; }

        public IEnumerable<int> CategoryIDs { get; set; } = new List<int>();

        public bool? SortAsc { get; set; }

        public string SortBy { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }

        public int? Take { get; set; } = 50;

        public int? Skip { get; set; } = 0;
    }
}
