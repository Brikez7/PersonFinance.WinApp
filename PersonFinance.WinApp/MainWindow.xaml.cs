using MahApps.Metro.Controls;
using PersonFinance.WinApp.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonFinance.WinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static readonly TypeModel[] TypesEntities = new TypeModel[] { TypeModel.BankingAccount, TypeModel.InvestAccount, TypeModel.Expense, TypeModel.Contract, TypeModel.Cash, TypeModel.Income};
        private ChartsPage? ChartsPage { get; set; } 
        private ManagerPage? ManagerPage { get; set; }
        private OverallResultsPage? OverallResultsPage { get; set; }
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

        private void ChangesTables_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OverallResultsPage_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Navigate(OverallResultsPage);
        }

        private void MainPage_Navigated(object sender, NavigationEventArgs e)
        {

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
