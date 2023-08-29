using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.Excel;
using PersonFinance.WinApp.ModalWindows;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            BRSumView.Text = DTOEx<TModelDTO>.Sum(GridEntities.ItemsSource.Cast<TModelDTO>(), (int)ColumnIndex, Currency);
/*            int index = (int)ColumnIndex;

            var sourceList = GridEntities.ItemsSource as ObservableCollection<TModelDTO>;
            var dataRowViewList = sourceList!.Select(x => x as DataRowView);

            var isMoney = false*//*(sourceList.First())[index] as Money is not null*//*;
            
            decimal sum = 0m;
            foreach (DataRowView row in GridEntities.Items)
            {
                var value = row[index];

                if (value?.ToString() is null)
                    continue;
                
                var decimalValue = (decimal)value;
                var money = value as Money;

                if (isMoney)
                {
                    if (money!.Currency == Currency)
                    {
                        sum += money.Amount;
                    }
                }
                else
                {
                    sum += decimalValue;
                }
            }

            if (!isMoney)
                BRSumView.Text = sum.ToString();
            else
                BRSumView.Text = $"{sum} {Currency}";*/
        }

        protected override void SelectedCellValueIsNumber()
        {
            var item = GridEntities.CurrentCell.Item as TModelDTO;
            
            var currentColumn =  GridEntities.CurrentCell.Column;

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
    }
}
