using PersonFinance.API.Domain.Entities;
using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.Helpers;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PersonFinance.WinApp.PersonFinanceModels
{
    /// <summary>
    /// Логика взаимодействия для ExpenseWindow.xaml
    /// </summary>
    public class ExpenseWindowAdd : ExpenseWindow
    {
        public ExpenseWindowAdd(string ButtonName) : base(ButtonName)
        {
        }

        protected override void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = ((ModelExpenseDTO)Resources["model"]);
            _ = PersonFinanceClientAPI<ExpenseDTO, RequestNewExpense>.InsertAsync(new RequestNewExpense(model.UserName, model.Category, model.SubCategory, model.ExpenditureDate, MoneySpent.Money, model.PurposeSpending), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously); ;
            Close();
        }
    }
    public class ExpenseWindowUpdate : ExpenseWindow
    {
        public ExpenseWindowUpdate(string ButtonName, ExpenseDTO entity) : base(ButtonName)
        {
            ((ModelExpenseDTO)Resources["model"]).Set(entity);
            MoneySpent.SetMoney(entity.MoneySpent);
        }

        protected override void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = ((ModelExpenseDTO)Resources["model"]);
            _ = PersonFinanceClientAPI<ExpenseDTO, RequestNewExpense>.UpdateAsync(new ExpenseDTO(model.Id, model.UserName, model.Category, model.SubCategory, model.ExpenditureDate, MoneySpent.Money, model.PurposeSpending), CancellationToken.None).NoAwait().ContinueWith((t) => Close(), TaskContinuationOptions.ExecuteSynchronously); ;
            Close(); 
        }
    }
    public abstract partial class ExpenseWindow : Window
    {
        public ExpenseWindow(string ButtonName)
        {
            InitializeComponent();
            ButtonCommand.Content = ButtonName;
        }

        protected abstract void Button_Click(object sender, RoutedEventArgs e);
    }
}
