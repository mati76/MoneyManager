using System;

namespace TransactionsImporter.Model
{
    public class MappingRule
    {
        public Predicate<Transaction> Predicate { get; set; }
        public Category Category{ get; set; }
    }
}
