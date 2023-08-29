using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelContractDTO : INotifyPropertyChanged
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

        private string _otherPerson;
        public string OtherPerson
        {
            get { return _otherPerson; }
            set
            {
                _otherPerson = value;
                OnPropertyChanged("OtherPerson");
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

        private bool _returned;
        public bool Returned
        {
            get { return _returned; }
            set
            {
                _returned = value;
                OnPropertyChanged("Returned");
            }
        }

        private DateTime? _returnedDate;
        public DateTime? ReturnedDate
        {
            get { return _returnedDate; }
            set
            {
                _returnedDate = value;
                OnPropertyChanged("ReturnedDate");
            }
        }

        private TypeContract _typeContract;
        public TypeContract TypeContract
        {
            get { return _typeContract; }
            set
            {
                _typeContract = value;
                OnPropertyChanged("TypeContract");
            }
        }

        public ModelContractDTO() { }
        public ModelContractDTO(Guid id, string userName, string otherPerson, DateTimeOffset receiptDate, string interestRate, bool returned, DateTimeOffset? returnedDate, TypeContract typeContract)
        {
            Id = id;
            UserName = userName;
            OtherPerson = otherPerson;
            ReceiptDate = receiptDate.DateTime;
            InterestRate = interestRate  ;
            Returned = returned;
            ReturnedDate = returnedDate == null ? null : receiptDate.DateTime;
            TypeContract = typeContract;
        }
        public void Set(ContractDTO contractDTO) 
        {
            Id = contractDTO.Id;
            UserName = contractDTO.UserName;
            OtherPerson = contractDTO.OtherPerson;
            ReceiptDate = contractDTO.ReceiptDate.DateTime;
            InterestRate = contractDTO.InterestRate.ToString();
            Returned = contractDTO.Returned;
            ReturnedDate = contractDTO.ReturnedDate == null ? null : contractDTO.ReturnedDate.Value.DateTime;
            TypeContract = contractDTO.TypeContract;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}