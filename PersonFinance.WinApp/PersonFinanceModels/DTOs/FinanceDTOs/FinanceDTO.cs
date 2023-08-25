using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.FinanceDTOs
{
    public class FinanceDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public Guid? MoneyAccountId { get; set; } = null;
        public Guid? PersonId { get; set; } = null;
        public Money Money { get; set; } = null!;

        public FinanceDTO(Guid id, Guid? moneyAccountId, Guid? personId, Money money)
        {
            Id = id;
            MoneyAccountId = moneyAccountId;
            PersonId = personId;
            Money = money;
        }
    }
}
