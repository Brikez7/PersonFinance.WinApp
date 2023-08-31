using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PersonFinance.API.Domain.Entities;
using PersonFinance.WinApp.ClientsWebAPI;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonFinance.WinApp.Helpers
{
    public static class LineSeriesHelper
    {
        public static async Task<LineSeries[]> CreateBankLineSeries()
        {
            var bankingAccounts = (await PersonFinanceClientAPI<BankingAccountDTO, RequestNewBankingAccount>.GetAsync(CancellationToken.None)).Data!.OrderBy(x => x.DateStart).OrderBy(x => x.DateEnd);

            var lineBankDeposit = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Red, Title = "Bank deposit(without interest)" };
            var lineBankDepositProfit = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Orange, Title = "Bank deposit profit" };

            foreach (var credit in bankingAccounts)
            {
                lineBankDeposit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.DateStart.Date), Convert.ToDouble(credit.Money.Amount)));
                lineBankDepositProfit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.DateEnd.Date), Convert.ToDouble(credit.Money.Amount * credit.InterestRate)));
            }

            return new[] { lineBankDepositProfit, lineBankDeposit };
        }
        public static async Task<LineSeries[]> CreateInvestAccountLineSeries()
        {
            var investAccounts = (await PersonFinanceClientAPI<InvestAccountDTO, RequestNewInvestAccount>.GetAsync(CancellationToken.None)).Data!.OrderBy(x => x.DateStart).OrderBy(x => x.DateEnd);

            var lineBankDeposit = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Blue, Title = "the value of assets in the investment package was" };
            var lineBankDepositProfit = new LineSeries() { YAxisKey = "Money", Color = OxyColors.DarkBlue, Title = "profit of assets in the investment package profit of assets" };

            foreach (var credit in investAccounts)
            {
                lineBankDeposit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.DateStart.Date), Convert.ToDouble(credit.Money.Amount)));
                lineBankDepositProfit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.DateEnd.Date), Convert.ToDouble(credit.Money.Amount * credit.InterestRate)));
            }

            return new[] { lineBankDepositProfit, lineBankDeposit };
        }

        public static async Task<LineSeries[]> CreateContractLineSeries()
        {
            var contracts = (await PersonFinanceClientAPI<ContractDTO, RequestNewContract>.GetAsync(CancellationToken.None)).Data!.OrderBy(x => x.ReceiptDate).OrderBy(x => x.ReturnedDate);

            var credits = contracts.Where(x => x.TypeContract == TypeContract.Credit);
            var debts = contracts.Where(x => x.TypeContract == TypeContract.Debt);

            var lineMoneyLentCredits = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Black, Title = "Money lent" };
            var lineCreditReturnedMoneys = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Fuchsia, Title = "Money returned on the loan" };

            var lineDebtsIncurred = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Wheat, Title = "Debts received" };
            var lineRepaidDebts = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Green, Title = "Repaid debts" };

            foreach (var credit in credits)
            {
                lineMoneyLentCredits.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.ReceiptDate.DateTime), Convert.ToDouble(credit.MoneyCredit.Amount)));
                if (credit.Returned)
                    lineCreditReturnedMoneys.Points.Add(new DataPoint(DateTimeAxis.ToDouble(credit.ReturnedDate!.Value.Date), Convert.ToDouble(credit.MoneyCredit.Amount)));
            }

            foreach (var debt in debts)
            {
                lineDebtsIncurred.Points.Add(new DataPoint(DateTimeAxis.ToDouble(debt.ReceiptDate.DateTime), Convert.ToDouble(debt.MoneyCredit.Amount)));
                if (debt.Returned)
                    lineRepaidDebts.Points.Add(new DataPoint(DateTimeAxis.ToDouble(debt.ReturnedDate!.Value.Date), Convert.ToDouble(debt.MoneyCredit.Amount)));
            }

            return new[] { lineMoneyLentCredits, lineCreditReturnedMoneys, lineDebtsIncurred, lineRepaidDebts };
        }

        public static async Task<LineSeries> CreateExpenseLineSeries()
        {
            var expenses = (await PersonFinanceClientAPI<ExpenseDTO, RequestNewExpense>.GetAsync(CancellationToken.None)).Data!.OrderBy(x => x.ExpenditureDate);

            var lineExpenses = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Yellow, Title = "Expenses" };

            foreach (var expense in expenses)
            {
                lineExpenses.Points.Add(new DataPoint(DateTimeAxis.ToDouble(expense.ExpenditureDate.Date), Convert.ToDouble(expense.MoneySpent.Amount)));
            }

            return lineExpenses;
        }

        public static async Task<LineSeries> CreateIncomeLineSeries()
        {
            var expenses = (await PersonFinanceClientAPI<IncomeDTO, RequestNewIncome>.GetAsync(CancellationToken.None)).Data!.OrderBy(x => x.ReceiptDate);

            var lineExpenses = new LineSeries() { YAxisKey = "Money", Color = OxyColors.Gray, Title = "Incomes" };

            foreach (var expense in expenses)
            {
                lineExpenses.Points.Add(new DataPoint(DateTimeAxis.ToDouble(expense.ReceiptDate.Date), Convert.ToDouble(expense.MoneyReceived.Amount)));
            }

            return lineExpenses;
        }
        public static void AddRange(this PlotModel plotModel, IEnumerable<LineSeries> arrayLineSeries)
        {
            foreach (var lineSeries in arrayLineSeries)
            {
                plotModel.Series.Add(lineSeries);
            }
        }
        public static async Task<IEnumerable<LineSeries>> CreateAllLinesSeries()
        {
            var linesBankLineSeries = await CreateBankLineSeries();
            var linesInvestAccountLineSeries = await CreateInvestAccountLineSeries();
            var linesContractLineSeries = await CreateContractLineSeries();
            var lineExpenseLineSeries = await CreateExpenseLineSeries();
            var lineIncomeLineSeries = await CreateIncomeLineSeries();
            return new LineSeries[] { lineExpenseLineSeries, lineIncomeLineSeries }.Concat(linesBankLineSeries).Concat(linesInvestAccountLineSeries).Concat(linesContractLineSeries);
        }

        public static PlotModel CreateDefaultPlotModel()
        {
            var plotModel = new PlotModel();

            plotModel.Axes.Add(new DateTimeAxis() { Title = "Date" });

            plotModel.Axes.Add(new LinearAxis { Title = "Money", Key = "Money" });
            return plotModel;
        }
    }
}
