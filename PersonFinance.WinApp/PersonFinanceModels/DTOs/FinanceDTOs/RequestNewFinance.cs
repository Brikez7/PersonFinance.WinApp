using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.FinanceDTOs
{
    public class RequestNewFinance : BaseRequestNew
    {
        public Guid? MoneyAccountId { get; set; } = null;
        public Guid? PersonId { get; set; } = null;
        public Money Money { get; set; } = null!;

        public RequestNewFinance(Guid? moneyAccountId, Guid? personId, Money money)
        {
            MoneyAccountId = moneyAccountId;
            PersonId = personId;
            Money = money;
        }
    }
}
