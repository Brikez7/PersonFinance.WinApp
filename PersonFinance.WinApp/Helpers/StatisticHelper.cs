using PersonFinance.API.Domain.Entities;
using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonFinance.WinApp.Helpers
{
    public static class StatisticHelper
    {
        public static async Task<ModelStatistics> CreateStatisticAsync((int, int, int) date, bool isAllValues, CancellationToken cancellationToken)
        {
            var currentFuture = DateTimeOffset.UtcNow.AddDays(date.Item1).AddMonths(date.Item2).AddYears(date.Item3);
            var currentPast = DateTimeOffset.UtcNow.AddDays(-date.Item1).AddMonths(-date.Item2).AddYears(-date.Item3);

            var models1 = (await PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.GetAsync(cancellationToken)).Data?.Where(x => DTOEx<BankingAccountDTO>.WhereDate(x.DateEnd, currentPast, currentFuture) || DTOEx<BankingAccountDTO>.WhereDate(x.DateStart, currentPast, currentFuture) || isAllValues);
            var models2 = (await PersonFinanceClientAPI<CashDTO, RequestNewCash>.GetAsync(cancellationToken)).Data;
            var models3 = (await PersonFinanceClientAPI<ContractDTO, RequestNewContract>.GetAsync(cancellationToken)).Data?.Where(x => x.ReturnedDate is not null && DTOEx<BankingAccountDTO>.WhereDate((DateTimeOffset)x.ReturnedDate, currentPast, currentFuture) || DTOEx<BankingAccountDTO>.WhereDate(x.ReceiptDate, currentPast, currentFuture) || isAllValues);
            var models4 = (await PersonFinanceClientAPI<ExpenseDTO, RequestNewExpense>.GetAsync(cancellationToken)).Data?.Where(x => DTOEx<BankingAccountDTO>.WhereDate(x.ExpenditureDate, currentPast, currentFuture) || isAllValues);
            var models5 = (await PersonFinanceClientAPI<IncomeDTO, RequestNewIncome>.GetAsync(cancellationToken)).Data?.Where(x => DTOEx<BankingAccountDTO>.WhereDate(x.ReceiptDate, currentPast, currentFuture) || isAllValues);
            var models6 = (await PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.GetAsync(cancellationToken)).Data?.Where(x => DTOEx<BankingAccountDTO>.WhereDate(x.DateStart, currentPast, currentFuture) || DTOEx<BankingAccountDTO>.WhereDate(x.DateEnd, currentPast, currentFuture) || isAllValues);

            string name = models2?.FirstOrDefault()?.UserName ?? "undefined";

            string bankingIncomeMoney = string.Join(", ", models1!.GroupBy(x => x.Money.Currency).Select(x => $"{x.Sum(t => t.Money.Amount * t.InterestRate)} {x.Key}"));

            string cashesMoney = string.Join(", ", models2!.GroupBy(x => x.Money.Currency).Select(x => $"{x.Sum(t => t.Money.Amount)} {x.Key}"));

            var credits = models3?.Where(x => x.TypeContract == TypeContract.Credit);
            string contractsMoneyCredit = string.Join(", ", credits!.GroupBy(x => x.MoneyCredit.Currency).Select(x => $"{x.Sum(t => t.MoneyCredit.Amount)} {x.Key}"));
            string contractsCreditReturnedMoney = string.Join(", ", credits!.GroupBy(x => x.ReturnedMoney?.Currency).Select(x => $"{x.Sum(t => t?.ReturnedMoney?.Amount)} {x.Key}"));

            var debts = models3?.Where(x => x.TypeContract == TypeContract.Debt);
            string contractsDebtMoneyCredit = string.Join(", ", debts!.GroupBy(x => x.MoneyCredit.Currency).Select(x => $"{x.Sum(t => t.MoneyCredit.Amount)} {x.Key}"));
            string contractsDebtReturnedMoney = string.Join(", ", debts!.GroupBy(x => x.ReturnedMoney?.Currency).Select(x => $"{x.Sum(t => t?.ReturnedMoney?.Amount)} {x.Key}"));

            string expensesMoney = string.Join(", ", models4!.GroupBy(x => x.MoneySpent.Currency).Select(x => $"{x.Sum(t => t.MoneySpent.Amount)} {x.Key}"));

            string incomeMoney = string.Join(", ", models5!.GroupBy(x => x.MoneyReceived.Currency).Select(x => $"{x.Sum(t => t.MoneyReceived.Amount)} {x.Key}"));

            string investAccountMoney = string.Join(", ", models6!.GroupBy(x => x.Money.Currency).Select(x => $"{x.Sum(t => t.Money.Amount * t.InterestRate)} {x.Key}"));

            return   new ModelStatistics(name, bankingIncomeMoney, cashesMoney, contractsMoneyCredit, contractsCreditReturnedMoney, contractsDebtMoneyCredit, contractsDebtReturnedMoney, expensesMoney, incomeMoney, investAccountMoney) ;
        }
    }
}
