using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelBankingAccountDTO : INotifyPropertyChanged
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

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                OnPropertyChanged("BankName");
            }
        }

        private DateTime _dateStart = DateTime.UtcNow;
        public DateTime DateStart
        {
            get { return _dateStart; }
            set
            {
                _dateStart = value;
                OnPropertyChanged("DateStart");
            }
        }

        private DateTime _dateEnd = DateTime.UtcNow.AddYears(1);
        public DateTime DateEnd 
        {
            get { return _dateEnd; }
            set
            {
                _dateEnd = value;
                OnPropertyChanged("DateEnd");
            }
        }

        private string _interestRate;
        public string InterestRate
        {
            get { return _interestRate; }
            set
            {
                _interestRate = value;
                OnPropertyChanged("InterestRate");
            }
        }

        public ModelBankingAccountDTO()
        {
        }

        public ModelBankingAccountDTO(Guid id, string userName, string bankName, DateTime dateStart, DateTime dateEnd, string interestRate)
        {
            Id = id;
            UserName = userName;
            BankName = bankName;
            DateStart = dateStart;
            DateEnd = dateEnd;
            InterestRate = interestRate;
        }

        public void Set(BankingAccountDTO bankingAccountDTO)
        {
            Id = bankingAccountDTO.Id;
            UserName = bankingAccountDTO.UserName;
            BankName = bankingAccountDTO.BankName;
            DateStart = bankingAccountDTO.DateStart.DateTime;
            DateEnd = bankingAccountDTO.DateEnd.DateTime;
            InterestRate =  bankingAccountDTO.InterestRate.ToString();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
