using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonFinance.WinApp.PersonFinanceModels
{
    /// <summary>
    /// Логика взаимодействия для MoneyControl.xaml
    /// </summary>
    public partial class MoneyControl : UserControl
    {
        public MoneyControl()
        {
            InitializeComponent();
            CCurrency.ItemsSource = Enum.GetValues(typeof(Currency)).Cast<Currency>();
        }
    }

    public partial class MoneyControl 
    {
        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register(
            "Amount",
            typeof(decimal),
            typeof(MoneyControl),
            new PropertyMetadata(0m));
        public decimal Amount
        {
            get { return (decimal)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public static readonly DependencyProperty CurrencyProperty = DependencyProperty.Register(
            "Currency",
            typeof(Currency),
            typeof(MoneyControl),
            new PropertyMetadata(Currency.RUB));
        public Currency Currency
        {
            get { return (Currency)GetValue(CurrencyProperty); }
            set { SetValue(CurrencyProperty, value); }
        }

        public Money Money => new(Amount, Currency);
        public void SetMoney(Money money)
        {
            Amount = money.Amount;
            Currency = money.Currency;
        }
    }
}

