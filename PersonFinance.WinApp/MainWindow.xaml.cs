using MahApps.Metro.Controls;
using PersonFinance.WinApp.Pages;
using System.Windows;
using System.Windows.Controls;

namespace PersonFinance.WinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static readonly TypeModel[] TypesEntities = new TypeModel[] { TypeModel.BankingAccount, TypeModel.InvestAccount, TypeModel.Expense, TypeModel.Contract, TypeModel.Cash, TypeModel.Income};
        private ChartPage ChartsPage { get; set; } = new ChartPage();
        private ManagerPage? ManagerPage { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ChoicePage.ItemsSource = TypesEntities;
            ChoicePage.SelectedItem = TypeModel.Cash;
        }
        private void OpenCharts_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(ChartsPage);
        }
        private void ChoicePage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var typeItem = (TypeModel)ChoicePage.SelectedItem;
            ManagerPage = FabricPages.CreateManagerPage(typeItem);
            MainPage.Navigate(ManagerPage);
        }

        private void ButtonChangesTables_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(ManagerPage);
        }
    }
}
