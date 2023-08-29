using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public class IncomeDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Money MoneyReceived { get; set; } 
        public DateTimeOffset ReceiptDate { get; set; }
        public string TypeActivity { get; set; }

        public IncomeDTO(Guid id, string userName, Money moneyReceived, DateTimeOffset receiptDate, string typeActivity)
        {
            Id = id;
            UserName = userName;
            MoneyReceived = moneyReceived;
            ReceiptDate = receiptDate;
            TypeActivity = typeActivity;
        }
    }
}
