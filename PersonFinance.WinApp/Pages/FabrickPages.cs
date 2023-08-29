using PersonFinance.API.Domain.Entities;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using NonGenerateManagerPage = PersonFinance.WinApp.ManagerPage;

namespace PersonFinance.WinApp.Pages
{
    public class FabricPages
    {
        public static NonGenerateManagerPage CreateManagerPage(TypeModel typeModel)
            => typeModel switch
        {
            TypeModel.Expense => new ManagerPage<ExpenseDTO, RequestNewExpense>(),
            TypeModel.Cash => new ManagerPage<CashDTO, RequestNewCash>(),
            TypeModel.BankingAccount => new ManagerPage<BankingAccountDTO, RequestNewBankingAccount>(),
            TypeModel.InvestAccount => new ManagerPage<InvestAccountDTO, RequestNewInvestAccount>(),
            TypeModel.Contract => new ManagerPage<ContractDTO, RequestNewContract>(),
            TypeModel.Income => new ManagerPage<IncomeDTO, RequestNewContract>(),
            _ => throw new NotImplementedException("this type not supposed")
        };
    }
}
