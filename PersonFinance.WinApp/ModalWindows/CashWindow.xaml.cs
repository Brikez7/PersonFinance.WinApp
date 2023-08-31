using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using PersonFinance.WinApp.Helpers;

namespace PersonFinance.WinApp.PersonFinanceModels
{
    /// <summary>
    /// Логика взаимодействия для CashWindow.xaml
    /// </summary>
    public class CashWindowAdd : CashWindow
    {
        public CashWindowAdd(string buttonName) : base(buttonName)
        {
        }

        protected override void Button_Click(object sender, RoutedEventArgs e)
        {
            ModelCashDTO model = (ModelCashDTO)Resources["model"];
            PersonFinanceClientAPI<CashDTO, RequestNewCash>.InsertAsync(new RequestNewCash(model.UserName, MoneyCash.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
        }
    }
    public class CashWindowUpdate : CashWindow
    {
        public CashWindowUpdate(string buttonName, CashDTO cashDTO) : base(buttonName)
        {
            ((ModelCashDTO)Resources["model"]).Set(cashDTO);
            MoneyCash.SetMoney(cashDTO.Money);
        }

        protected override void Button_Click(object sender, RoutedEventArgs e)
        {
            ModelCashDTO model = (ModelCashDTO)Resources["model"];
            PersonFinanceClientAPI<CashDTO, RequestNewCash>.UpdateAsync(new CashDTO(model.Id, model.UserName, MoneyCash.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
        }
    }
    public abstract partial class CashWindow : Window
    {
        public CashWindow(string buttonName)
        {
            InitializeComponent();
            ButtonCommand.Content = buttonName;
        }
        protected abstract void Button_Click(object sender, RoutedEventArgs e);
    }
}
