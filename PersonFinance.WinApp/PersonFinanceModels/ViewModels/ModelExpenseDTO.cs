using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelExpenseDTO : INotifyPropertyChanged
    {
        public Guid Id;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        private string _subCategory;
        public string SubCategory
        {
            get { return _subCategory; }
            set
            {
                _subCategory = value;
                OnPropertyChanged("SubCategory");
            }
        }

        private DateTime _expenditureDate = DateTime.UtcNow;
        public DateTime ExpenditureDate
        {
            get { return _expenditureDate; }
            set
            {
                _expenditureDate = value;
                OnPropertyChanged("ExpenditureDate");
            }
        }

        private string _purposeSpending;
        public string PurposeSpending
        {
            get { return _purposeSpending; }
            set
            {
                _purposeSpending = value;
                OnPropertyChanged("PurposeSpending");
            }
        }

        public ModelExpenseDTO() { }
        public ModelExpenseDTO(Guid id, string userName, string category, string subCategory, DateTimeOffset expenditureDate, string purposeSpending)
        {
            Id = id;
            UserName = userName;
            Category = category;
            SubCategory = subCategory;
            ExpenditureDate = expenditureDate.DateTime;
            PurposeSpending = purposeSpending;
        }
        public void Set(ExpenseDTO expenseDTO) 
        {
            Id = expenseDTO.Id;
            UserName = expenseDTO.UserName;
            Category = expenseDTO.Category;
            SubCategory = expenseDTO.SubCategory;
            ExpenditureDate = expenseDTO.ExpenditureDate.DateTime;
            PurposeSpending = expenseDTO.PurposeSpending;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
