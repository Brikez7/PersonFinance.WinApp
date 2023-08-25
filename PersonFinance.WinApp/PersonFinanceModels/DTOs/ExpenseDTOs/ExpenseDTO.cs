using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.ExpenseDTOs
{
    public class ExpenseDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public DateTimeOffset ExpenditureDate { get; set; }
        public Money MoneySpent { get; set; }
        public string PurposeSpending { get; set; }

        public ExpenseDTO(Guid id, Guid personId, string category, string subCategory, DateTimeOffset expenditureDate, Money moneySpent, string purposeSpending)
        {
            Id = id;
            PersonId = personId;
            Category = category;
            SubCategory = subCategory;
            ExpenditureDate = expenditureDate;
            MoneySpent = moneySpent;
            PurposeSpending = purposeSpending;
        }
    }
}
