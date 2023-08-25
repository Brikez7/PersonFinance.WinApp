using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.Enums;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.ContractDTOs
{
    public class RequestNewContract : BaseRequestNew
    {
        public Guid PersonId { get; set; }
        public string OtherPerson { get; set; }
        public DateTimeOffset ReceiptDate { get; set; }
        public decimal InterestRate { get; set; }
        public Money MoneyCredit { get; set; }
        public TypeContract TypeContract { get; set; }

        public RequestNewContract(Guid personId, string otherPerson, DateTimeOffset receiptDate, decimal interestRate, Money moneyCredit, TypeContract typeContract)
        {
            PersonId = personId;
            OtherPerson = otherPerson;
            ReceiptDate = receiptDate;
            InterestRate = interestRate;
            MoneyCredit = moneyCredit;
            TypeContract = typeContract;
        }
    }
}