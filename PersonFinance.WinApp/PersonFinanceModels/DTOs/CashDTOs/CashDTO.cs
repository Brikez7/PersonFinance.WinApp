using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public class CashDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public Money Money { get; set; }
        public CashDTO(Guid id, string userName, Money money)
        {
            Id = id;
            UserName = userName;
            Money = money;
        }
    }
}
