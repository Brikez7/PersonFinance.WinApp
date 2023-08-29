using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelCashDTO : INotifyPropertyChanged
    {
        public Guid Id { get; set; }
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
        public ModelCashDTO() { }
        public ModelCashDTO(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
        }
        public void Set(CashDTO cashDTO) 
        {
            Id = cashDTO.Id;
            UserName = cashDTO.UserName;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
