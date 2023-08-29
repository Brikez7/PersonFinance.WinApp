using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.Excel;
using PersonFinance.WinApp.ModalWindows;
using PersonFinance.WinApp.Pages;
using PersonFinance.WinApp.PersonFinanceModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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
    }
    public abstract partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        protected abstract void ButtonRefresh_Click_1(object sender, RoutedEventArgs e);
        protected abstract void GetExcel_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonAdd_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonUpdate_Click(object sender, RoutedEventArgs e);
        protected abstract void ButtonDelete_Click(object sender, RoutedEventArgs e);
    }
}
