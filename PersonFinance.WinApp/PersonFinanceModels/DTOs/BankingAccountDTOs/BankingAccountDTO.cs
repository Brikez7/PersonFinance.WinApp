using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;
using System.ComponentModel;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public class BankingAccountDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string BankName { get; set; } 
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }
        public decimal InterestRate { get; set; }
        public Money Money { get; set; }

        public BankingAccountDTO()
        {
        }

        public BankingAccountDTO(Guid id, string userName, string bankName, DateTimeOffset dateStart, DateTimeOffset dateEnd, decimal interestRate, Money money)
        {
            Id = id;
            UserName = userName;
            BankName = bankName;
            DateStart = dateStart;
            DateEnd = dateEnd;
            InterestRate = interestRate;
            Money = money;
        }
    }
}
