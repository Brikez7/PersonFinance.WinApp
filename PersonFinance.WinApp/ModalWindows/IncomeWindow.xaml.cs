using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PersonFinance.WinApp.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для IncomeWindow.xaml
    /// </summary>
    public class IncomeWindowUpdate : IncomeWindow
    {
        public IncomeWindowUpdate(string nameButton, IncomeDTO incomeDTO) : base(nameButton)
        {
            ((ModelIncomeDTO)Resources["model"]).Set(incomeDTO);
            MoneyReceived.SetMoney(incomeDTO.MoneyReceived);
        }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            var model = (ModelIncomeDTO)Resources["model"];
            _ = PersonFinanceClientAPI<IncomeDTO, RequestNewIncome>.UpdateAsync(new IncomeDTO(model.Id, model.UserName, MoneyReceived.Money, model.ReceiptDate, model.TypeActivity), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public class IncomeWindowAdd : IncomeWindow
    {
        public IncomeWindowAdd(string nameButton) : base(nameButton)
        {
        }

        protected override void ButtonCommand_Click(object sender, RoutedEventArgs e)
        {
            var model = (ModelIncomeDTO)Resources["model"];
            _ = PersonFinanceClientAPI<IncomeDTO, RequestNewIncome>.InsertAsync(new RequestNewIncome(model.UserName, MoneyReceived.Money, model.ReceiptDate, model.TypeActivity), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
    public abstract partial class IncomeWindow : Window
    {
        public IncomeWindow(string nameButton)
        {
            InitializeComponent();
            ButtonCommand.Content = nameButton;
        }

        protected abstract void ButtonCommand_Click(object sender, RoutedEventArgs e);
    }
}
