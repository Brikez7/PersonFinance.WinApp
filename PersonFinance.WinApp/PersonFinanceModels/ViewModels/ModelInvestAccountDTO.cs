using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelInvestAccountDTO : INotifyPropertyChanged
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

        public ModelInvestAccountDTO()
        {
        }

        public ModelInvestAccountDTO(Guid id, string userName, DateTime dateStart, DateTime dateEnd, string interestRate)
        {
            Id = id;
            UserName = userName;
            DateStart = dateStart;
            DateEnd = dateEnd;
            InterestRate = interestRate;
        }

        public void Set(InvestAccountDTO investAccountDTO)
        {
            Id = investAccountDTO.Id;
            UserName = investAccountDTO.UserName;
            DateStart = investAccountDTO.DateStart.DateTime;
            DateEnd = investAccountDTO.DateEnd.DateTime;
            InterestRate = investAccountDTO.InterestRate.ToString();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}