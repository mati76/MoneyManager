namespace TransactionsImporter.Model
{
    public class TransactionSplit : ModelBase
    {
        private decimal _amountX;
        public decimal AmountX
        {
            get => _amountX;
            set
            {
                _amountX = value;
                OnPropertyChanged("AmountX");
                OnPropertyChanged("AmountY");
            }
        }
        public decimal AmountY => Transaction.Amount - AmountX;
        public Model.Transaction Transaction { get; set; }
    }
}
