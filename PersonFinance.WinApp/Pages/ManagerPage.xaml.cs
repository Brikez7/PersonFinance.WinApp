using PersonFinance.API.Domain.Entities;
using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.Excel;
using PersonFinance.WinApp.Helpers;
using PersonFinance.WinApp.ModalWindows;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PersonFinance.WinApp
{
    public partial class ManagerPage<TModelDTO, TRequest> : ManagerPage where TModelDTO : BaseDTO where TRequest : BaseRequestNew
    {
        public CancellationToken CancellationToken { get; set; } = new CancellationToken();
        public ManagerPage()
        {
            InitializeComponent();
        }

        protected override void ButtonRefresh_Click_1(object sender, RoutedEventArgs e)
        {
            _ = RefreshAsync(CancellationToken);
            ColumnIndex = null;
        }
        public async Task RefreshAsync(CancellationToken cancellationToken)
        {
            var models = await PersonFinanceClientAPI<TModelDTO, TRequest>.GetAsync(cancellationToken);
            GridEntities.ItemsSource = models.Data;
        }
        protected override void GetExcel_Click(object sender, RoutedEventArgs e)
        {
            var excelReport = new ExcelReport<TModelDTO>();
            excelReport.FormReportExcel(GridEntities.ItemsSource.Cast<TModelDTO>().AsEnumerable());
        }

        protected override void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var expenseWindowAdd = FabricModalWindows.CreateModalWindowsAdd<TModelDTO>();
            expenseWindowAdd.Show();
        }

        protected override void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = (TModelDTO)GridEntities.SelectedItem;
            var expenseWindowAdd = FabricModalWindows.CreateModalWindowsUpdate(selectedValue);
            expenseWindowAdd.Show();
        }

        protected override void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = (TModelDTO)GridEntities.SelectedItem;
            _ = Delete(selectedValue);
        }
        private async Task Delete(TModelDTO model)
        {
            await PersonFinanceClientAPI<TModelDTO, TRequest>.DeleteAsync(model, CancellationToken.None);
            var models = await PersonFinanceClientAPI<TModelDTO, TRequest>.GetAsync(CancellationToken.None);
            GridEntities.ItemsSource = models.Data;
        }

        protected override void ButtonSum_Click(object sender, RoutedEventArgs e)
        {
            if (ColumnIndex is null)
                return;

            if (GridEntities.SelectedItems.Count > 1)
            {
                BRSumView.Text = DTOEx<TModelDTO>.Sum(GridEntities.SelectedItems.Cast<TModelDTO>(), (int)ColumnIndex, Currency);
                return;
            }

            BRSumView.Text = DTOEx<TModelDTO>.Sum(GridEntities.ItemsSource.Cast<TModelDTO>(), (int)ColumnIndex, Currency);
        }

        protected override void SelectedCellValueIsNumber()
        {
            var item = GridEntities.CurrentCell.Item as TModelDTO;

            var currentColumn = GridEntities.CurrentCell.Column;

            if (currentColumn == null)
                return;

            var nameColumn = currentColumn.Header;

            if (item is null)
                return;

            if (nameColumn?.ToString() == null)
                return;

            var ColumnName = nameColumn.ToString()!;

            object result = item.GetType().GetProperty(ColumnName)!.GetValue(item, null)!;

            if (result == null)
                return;

            if (result.GetType() == typeof(Money) || result.GetType() == typeof(decimal))
                ColumnIndex = currentColumn.DisplayIndex;

            Currency = (result as Money)?.Currency;
        }
    }
    public abstract partial class ManagerPage : Page
    {
        public int? ColumnIndex = 0;
        public Currency? Currency;
        public ManagerPage()
        {
            InitializeComponent();
        }

        protected abstract void ButtonRefresh_Click_1(object sender, RoutedEventArgs e);
        protected abstract void GetExcel_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonAdd_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonUpdate_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonDelete_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonSum_Click(object sender, RoutedEventArgs e);
        protected abstract void SelectedCellValueIsNumber();
        private void GridEntities_CurrentCellChanged(object sender, System.EventArgs e)
        {
            SelectedCellValueIsNumber();
        }
        private void ButtonViewsYear_Click(object sender, RoutedEventArgs e)
        {
            _ = ResultsForAsync(new(0, 0, 1), CancellationToken.None);
        }
        private void ButtonViewsMonth_Click(object sender, RoutedEventArgs e)
        {
            _ = ResultsForAsync((0, 1, 0), CancellationToken.None);
        }
        private void ButtonViewsDashboard_Click(object sender, RoutedEventArgs e)
        {
            _ = ResultsForAsync((0, 0, 0), CancellationToken.None, true);
        }
        private void ButtonViewsWeek_Click(object sender, RoutedEventArgs e)
        {
            _ = ResultsForAsync((1, 0, 0), CancellationToken.None);
        }
        public async Task ResultsForAsync((int, int, int) Date, CancellationToken cancellationToken, bool all = false)
        {
            GridEntities.ItemsSource = new[] { await StatisticHelper.CreateStatisticAsync(Date, all, cancellationToken) };
        }

        private void ButtonNetProfit_Click(object sender, RoutedEventArgs e)
        {
            _ = CalculateNetProfit();
        }
        public async Task CalculateNetProfit() 
        {
            var bankNetProfit = (await PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.InterestRate * x.Money.Amount);
            var incomeProfit = (await PersonFinanceClientAPI<IncomeDTO, RequestNewIncome>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.MoneyReceived.Amount);
            var investNetProfit = (await PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.Money.Amount * x.InterestRate);

            var contracts = (await PersonFinanceClientAPI<ContractDTO, RequestNewContract>.GetAsync(CancellationToken.None)).Data!;
            var credits = contracts.Where(x => x.TypeContract == TypeContract.Credit);
            var debts = contracts.Where(x => x.TypeContract == TypeContract.Debt);

            var debtsExpense = debts.Sum(x => x.MoneyCredit.Amount - x.ReturnedMoney?.Amount);
            var creditsProfit = contracts.Sum(x => x.MoneyCredit.Amount - x.ReturnedMoney?.Amount);

            var expense = (await PersonFinanceClientAPI<ExpenseDTO, RequestNewExpense>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.MoneySpent.Amount);

            RBNetProfit.Text = (bankNetProfit + incomeProfit + investNetProfit + creditsProfit - debtsExpense - expense).ToString();
        }

        public async Task CalculateBalance()
        {
            var bankBalance = (await PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.Money.Amount);
            var investBalance = (await PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.Money.Amount);
            var cash = (await PersonFinanceClientAPI<CashDTO, RequestNewCash>.GetAsync(CancellationToken.None)).Data!.Sum(x => x.Money.Amount);

            var contracts = (await PersonFinanceClientAPI<ContractDTO, RequestNewContract>.GetAsync(CancellationToken.None)).Data!;
            var credits = contracts.Where(x => x.TypeContract == TypeContract.Credit);
            var debts = contracts.Where(x => x.TypeContract == TypeContract.Debt);

            var debtsExpense = debts.Where(x => x.Returned!).Sum(x => x.MoneyCredit.Amount);
            var creditsBalance = contracts.GroupBy(x => x.Returned).Sum((x) => 
            {
                if (x.Key)
                    return x.Sum(t => t.MoneyCredit.Amount - t.ReturnedMoney?.Amount);

                return 0;
            });

            RBBalance.Text = (bankBalance + investBalance + cash - debtsExpense + creditsBalance).ToString();
        }

        private void ButtonBalance_Click(object sender, RoutedEventArgs e)
        {
            _ = CalculateBalance();
        }
    }
}
