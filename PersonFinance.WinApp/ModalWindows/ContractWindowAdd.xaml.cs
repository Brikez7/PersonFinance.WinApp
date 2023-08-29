using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PersonFinance.WinApp.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для ContractWindowUpdate.xaml
    /// </summary>
    public partial class ContractWindowAdd : Window
    {
        public ContractWindowAdd(string nameCommand)
        {
            InitializeComponent();
            CTypeContract.ItemsSource = Enum.GetValues(typeof(TypeContract)).Cast<TypeContract>();
            ButtonCommand.Content = nameCommand;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ModelContractDTO model = (ModelContractDTO)Resources["model"];
            _ = PersonFinanceClientAPI<ContractDTO, RequestNewContract>.InsertAsync(new RequestNewContract(model.UserName, model.OtherPerson, model.ReceiptDate, decimal.Parse(model.InterestRate), MoneyCredit.Money, (TypeContract)model.TypeContract), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
}
