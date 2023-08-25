using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.IncomeDTOs
{
    public class RequestNewIncome : BaseRequestNew
    {
        public Guid PersonId { get; set; }
        public Money MoneyReceived { get; set; }
        public DateTimeOffset ReceiptDate { get; set; }
        public string TypeActivity { get; set; }

        public RequestNewIncome(Guid personId, Money moneyReceived, DateTimeOffset receiptDate, string typeActivity)
        {
            PersonId = personId;
            MoneyReceived = moneyReceived;
            ReceiptDate = receiptDate;
            TypeActivity = typeActivity;
        }
    }
}
