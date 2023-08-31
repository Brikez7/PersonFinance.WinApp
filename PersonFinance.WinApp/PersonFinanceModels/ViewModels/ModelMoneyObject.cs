using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonFinance.WinApp.PersonFinanceModels.ViewModels
{
    public class ModelMoneyObject : INotifyPropertyChanged
    {
        public ModelMoneyObject() { }
        public ModelMoneyObject(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        private decimal _amount = 0;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public Currency _currency = Currency.RUB;
        public Currency Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                OnPropertyChanged("Currency");
            }
        }

        public void SetMoney(Money money)
        {
            Amount = money.Amount;
            Currency = money.Currency;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
