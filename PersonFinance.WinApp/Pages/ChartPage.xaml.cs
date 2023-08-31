using OxyPlot;
using PersonFinance.WinApp.Helpers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PersonFinance.WinApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();
        }

        private void ButtonCreateCharts_Click(object sender, RoutedEventArgs e)
        {
            _ = Task();
        }
        public async Task Task()
        {
            PlotModel plotModel = LineSeriesHelper.CreateDefaultPlotModel();

            plotModel.AddRange(await LineSeriesHelper.CreateAllLinesSeries());

            plotView.Model = plotModel;
        }
    }
}
