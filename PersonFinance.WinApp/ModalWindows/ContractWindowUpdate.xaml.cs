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
    public partial class ContractWindowUpdate : Window
    {
        public ContractWindowUpdate(string nameCommand, ContractDTO contractDTO)
        {
            InitializeComponent();
            CTypeContract.ItemsSource = Enum.GetValues(typeof(TypeContract)).Cast<TypeContract>();
            ButtonCommand.Content = nameCommand;

            ((ModelContractDTO)Resources["model"]).Set(contractDTO);

            MoneyCredit.SetMoney(contractDTO.MoneyCredit);
            if(contractDTO.ReturnedMoney != null)
                ReturnedMoney.SetMoney(contractDTO.ReturnedMoney);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = ((ModelContractDTO)Resources["model"]);
            _ = PersonFinanceClientAPI<ContractDTO, RequestNewContract>.UpdateAsync(new ContractDTO(model.Id, model.UserName, model.OtherPerson, model.ReceiptDate, decimal.Parse(model.InterestRate), MoneyCredit.Money, model.Returned, model.ReturnedDate, ReturnedMoney.Money, model.TypeContract), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously);
            Close();
        }
    }
}
