using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.IncomeDTOs
{
    public class IncomeDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Money MoneyReceived { get; set; }
        public DateTimeOffset ReceiptDate { get; set; }
        public string TypeActivity { get; set; }

        public IncomeDTO(Guid id, Guid personId, Money moneyReceived, DateTimeOffset receiptDate, string typeActivity)
        {
            Id = id;
            PersonId = personId;
            MoneyReceived = moneyReceived;
            ReceiptDate = receiptDate;
            TypeActivity = typeActivity;
        }
    }
}
