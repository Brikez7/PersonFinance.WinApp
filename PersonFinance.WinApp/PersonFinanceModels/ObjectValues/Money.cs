namespace PersonFinance.WinApp.PersonFinanceModels.ObjectValues
{
    public class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
    public enum Currency
    {
        RUB,
        BYN,
        USD,
        EUR,
        CNY
    }
}
