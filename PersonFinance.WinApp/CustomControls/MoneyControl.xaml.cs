using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System;
using System.Linq;
using System.Windows.Controls;

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
        public void SetMoney(Money money) 
        {
            ((ModelMoneyObject)Resources["model"]).SetMoney(money);
        }
        public Money Money => new(((ModelMoneyObject)Resources["model"]).Amount, ((ModelMoneyObject)Resources["model"]).Currency);
    }
}

