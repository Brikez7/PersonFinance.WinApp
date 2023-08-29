using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public static class DTOEx<ModelDTO> where ModelDTO : BaseDTO
    {
        public static string Sum(IEnumerable<ModelDTO> models, int index, Currency? currency)
        => typeof(ModelDTO) switch
        {
            var _ when typeof(ModelDTO) == typeof(CashDTO) => $"{models.Cast<CashDTO>().Where(x => x.Money.Currency == currency).Sum(x => x.Money.Amount)} {currency}",

            var _ when typeof(ModelDTO) == typeof(ContractDTO) && index == 5 => $"{models.Cast<ContractDTO>().Where(x => x.MoneyCredit.Currency == currency).Sum(x => x.MoneyCredit.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(ContractDTO) && index == 9 => $"{models.Cast<ContractDTO>().Where(x => x.ReturnedMoney?.Currency == currency).Sum(x => x.ReturnedMoney?.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(ContractDTO) && index == 4 => $"{models.Cast<ContractDTO>().Sum(x => x.InterestRate)}",
            var _ when typeof(ModelDTO) == typeof(BankingAccountDTO) && index == 6 => $"{models.Cast<BankingAccountDTO>().Where(x => x.Money.Currency == currency).Sum(x => x.Money.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(BankingAccountDTO) && index == 5 => $"{models.Cast<BankingAccountDTO>().Sum(x => x.InterestRate)}",
            var _ when typeof(ModelDTO) == typeof(ExpenseDTO) && index == 5 => $"{models.Cast<ExpenseDTO>().Where(x => x.MoneySpent.Currency == currency).Sum(x => x.MoneySpent.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(IncomeDTO) && index == 2 => $"{models.Cast<IncomeDTO>().Where(x => x.MoneyReceived.Currency == currency).Sum(x => x.MoneyReceived.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(InvestAccountDTO) && index == 5 => $"{models.Cast<InvestAccountDTO>().Where(x => x.Money.Currency == currency).Sum(x => x.Money.Amount)} {currency}",
            var _ when typeof(ModelDTO) == typeof(InvestAccountDTO) && index == 4 => $"{models.Cast<InvestAccountDTO>().Sum(x => x.InterestRate)}",
            _ => throw new NotImplementedException("this type not supposed"),
        };        
    }
}
