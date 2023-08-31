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
    /// Логика взаимодействия для BankingAccountWindow.xaml
    /// </summary>
    public class BankingAccountWindowUpdate : BankingAccountWindow
    {
        public BankingAccountWindowUpdate(string nameButton, BankingAccountDTO bankingAccountDTO) : base(nameButton)
        {
            BankingMoney.SetMoney(bankingAccountDTO.Money);
            ((ModelBankingAccountDTO)Resources["model"]).Set(bankingAccountDTO);
        }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            ModelBankingAccountDTO model = (ModelBankingAccountDTO)Resources["model"];
            PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.UpdateAsync(new BankingAccountDTO(model.Id, model.UserName, model.BankName, model.DateStart, model.DateEnd, decimal.Parse(model.InterestRate), BankingMoney.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public class BankingAccountWindowAdd : BankingAccountWindow
    {
        public BankingAccountWindowAdd(string nameButton) : base(nameButton)
        {
        }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            ModelBankingAccountDTO model = (ModelBankingAccountDTO)Resources["model"];
            PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.InsertAsync(new RequestNewBankingAccount(model.UserName, model.BankName, model.DateStart, model.DateEnd, decimal.Parse(model.InterestRate), BankingMoney.Money), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public abstract partial class BankingAccountWindow : Window
    {
        public BankingAccountWindow(string nameButton)
        {
            InitializeComponent();
            ButtonCommand.Content = nameButton;
        }

        protected abstract void ButtonCommand_Click(object sender, RoutedEventArgs e);
    }
}
