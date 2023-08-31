using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.Helpers;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PersonFinance.WinApp.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для InvestAccountWindow.xaml
    /// </summary>
    public class InvestAccountWindowUpdate : InvestAccountWindow
    {
        public InvestAccountWindowUpdate(string nameCommand, InvestAccountDTO investAccountDTO) : base(nameCommand)
        {
            ((ModelInvestAccountDTO)Resources["model"]).Set(investAccountDTO);
            MonetaryEquivalent.SetMoney(investAccountDTO!.Money);
        }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            var model = (ModelInvestAccountDTO)Resources["model"];
            _ = PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.UpdateAsync(new InvestAccountDTO(model.Id, model.UserName, model.DateStart, model.DateEnd, decimal.Parse(model.InterestRate), MonetaryEquivalent.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public class InvestAccountWindowAdd : InvestAccountWindow
    {
        public InvestAccountWindowAdd(string nameCommand) : base(nameCommand){ }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            var model = (ModelInvestAccountDTO)Resources["model"];
            _ = PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.InsertAsync(new RequestNewInvestAccount(model.UserName, model.DateStart, model.DateEnd, decimal.Parse(model.InterestRate), MonetaryEquivalent.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public abstract partial class InvestAccountWindow : Window
    {
        public InvestAccountWindow(string nameCommand)
        {
            InitializeComponent();
            ButtonCommand.Content = nameCommand;
        }

        protected abstract void ButtonCommand_Click(object sender, RoutedEventArgs e);
    }
}
