using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelIncomeDTO : INotifyPropertyChanged
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

        private DateTime _receiptDate = DateTime.UtcNow;
        public DateTime ReceiptDate
        {
            get { return _receiptDate; }
            set
            {
                _receiptDate = value;
                OnPropertyChanged("ReceiptDate");
            }
        }

        private string _typeActivity;
        public string TypeActivity
        {
            get { return _typeActivity; }
            set
            {
                _typeActivity = value;
                OnPropertyChanged("TypeActivity");
            }
        }

        public ModelIncomeDTO() { }
        public ModelIncomeDTO(Guid id, string userName, DateTimeOffset receiptDate, string typeActivity)
        {
            Id = id;
            UserName = userName;
            ReceiptDate = receiptDate.DateTime;
            TypeActivity = typeActivity;
        }
        public void Set(IncomeDTO incomeDTO) 
        {
            Id = incomeDTO.Id;
            UserName = incomeDTO.UserName;
            ReceiptDate = incomeDTO.ReceiptDate.DateTime;
            TypeActivity = incomeDTO.TypeActivity;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
