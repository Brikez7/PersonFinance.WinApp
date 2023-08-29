using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public class RequestNewIncome : BaseRequestNew
    {
        public string UserName { get; set; } 
        public Money MoneyReceived { get; set; } 
        public DateTimeOffset ReceiptDate { get; set; }
        public string TypeActivity { get; set; } 

        public RequestNewIncome(string userName, Money moneyReceived, DateTimeOffset receiptDate, string typeActivity)
        {
            UserName = userName;
            MoneyReceived = moneyReceived;
            ReceiptDate = receiptDate;
            TypeActivity = typeActivity;
        }
    }
}
