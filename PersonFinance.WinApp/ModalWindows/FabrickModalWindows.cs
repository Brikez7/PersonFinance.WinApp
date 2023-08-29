using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.Windows;

namespace PersonFinance.WinApp.ModalWindows
{
    public static class FabricModalWindows
    {
        public static Window CreateModalWindowsAdd<T>() where T : BaseDTO
            => typeof(T) switch
        {
            var _ when typeof(T) == typeof(ExpenseDTO) => new ExpenseWindowAdd("Add"),
            var _ when typeof(T) == typeof(CashDTO) => new CashWindowAdd("Add"),
            var _ when typeof(T) == typeof(BankingAccountDTO) => new BankingAccountWindowAdd("Add"),
            var _ when typeof(T) == typeof(InvestAccountDTO) => new InvestAccountWindowAdd("Add"),
            var _ when typeof(T) == typeof(ContractDTO) => new ContractWindowAdd("Add"),
            var _ when typeof(T) == typeof(IncomeDTO) => new IncomeWindowAdd("Add"),
            _ => throw new NotImplementedException("this type not supposed")
        };

        public static Window CreateModalWindowsUpdate<T>(T model) where T : BaseDTO
            => typeof(T) switch
        {
            var _ when typeof(T) == typeof(ExpenseDTO) => new ExpenseWindowUpdate("Update", model as ExpenseDTO),
            var _ when typeof(T) == typeof(CashDTO) => new CashWindowUpdate("Update", model as CashDTO),
            var _ when typeof(T) == typeof(BankingAccountDTO) => new BankingAccountWindowUpdate("Update", model as BankingAccountDTO),
            var _ when typeof(T) == typeof(InvestAccountDTO) => new InvestAccountWindowUpdate("Update", model as InvestAccountDTO),
            var _ when typeof(T) == typeof(ContractDTO) => new ContractWindowUpdate("Update", model as ContractDTO),
            var _ when typeof(T) == typeof(IncomeDTO) => new IncomeWindowUpdate("Update", model as IncomeDTO),
            _ => throw new NotImplementedException("this type not supposed")
        };
    }
}
