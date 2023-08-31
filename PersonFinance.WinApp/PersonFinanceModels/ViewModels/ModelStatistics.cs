namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelStatistics 
    {
        public string UserName { get; set; }
        public string BankDeposit { get; set; }
        public string PersonCash { get; set; }
        public string MoneyLentCredits { get; set; }
        public string CreditReturnedMoneys { get; set; }
        public string DebtsIncurred { get; set; }
        public string RepaidDebts { get; set; }
        public string ExpensesMoney { get; set; }
        public string Income { get; set; }
        public string InvestAccountMoney { get; set; }

        public ModelStatistics()
        {
        }

        public ModelStatistics(string userName, string bankDeposit, string personCash, string moneyLentCredits, string creditReturnedMoneys, string debtsIncurred, string repaidDebts, string expensesMoney, string income, string investAccountMoney)
        {
            UserName = userName;
            BankDeposit = bankDeposit;
            PersonCash = personCash;
            MoneyLentCredits = moneyLentCredits;
            CreditReturnedMoneys = creditReturnedMoneys;
            DebtsIncurred = debtsIncurred;
            RepaidDebts = repaidDebts;
            ExpensesMoney = expensesMoney;
            Income = income;
            InvestAccountMoney = investAccountMoney;
        }
    }
}
