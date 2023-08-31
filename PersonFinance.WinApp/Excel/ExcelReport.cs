using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using ExcelLicenseContext = OfficeOpenXml.LicenseContext;

namespace PersonFinance.WinApp.Excel
{
    public class ExcelReport<ModelDTO> where ModelDTO : BaseDTO
    {
        private static readonly char[] _alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        static ExcelReport() { ExcelPackage.LicenseContext = ExcelLicenseContext.NonCommercial; }

        private ExcelWorksheet _sheet;

        public void FormReportExcel(IEnumerable<ModelDTO> models)
        {
            var reportExcel = ReportExcel(models);

            File.WriteAllBytes($"{reportExcel.Item2}.xlsx", reportExcel.Item1);
        }

        private (byte[],string) ReportExcel(IEnumerable<ModelDTO> models)
            => typeof(ModelDTO) switch
            {
                var _ when typeof(ModelDTO) == typeof(CashDTO) => (CreateExcel(models.Cast<CashDTO>()), "Cashes"),
                var _ when typeof(ModelDTO) == typeof(ContractDTO) => (CreateExcel(models.Cast<ContractDTO>()), "Contracts"),
                var _ when typeof(ModelDTO) == typeof(ExpenseDTO) => (CreateExcel(models.Cast<ExpenseDTO>()), "Expenses"),
                var _ when typeof(ModelDTO) == typeof(IncomeDTO) => (CreateExcel(models.Cast<IncomeDTO>()), "Incomes"),
                var _ when typeof(ModelDTO) == typeof(InvestAccountDTO) => (CreateExcel(models.Cast<InvestAccountDTO>()), "InvestsAccounts"),
                var _ when typeof(ModelDTO) == typeof(BankingAccountDTO) => (CreateExcel(models.Cast<BankingAccountDTO>()), "BankingAccounts"),
                _ => throw new NotImplementedException("this type not supposed"),
            };

        private byte[] CreateExcel(IEnumerable<BankingAccountDTO> bankingAccounts)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("BankingAccounts");

            CreateHeader("Id", "User name", "Bank name", "Date start", "Date end", "Interest rate", "Money");
            Apply(bankingAccounts.Select(x => new string[] {
                x.Id.ToString(), 
                x.UserName, 
                x.BankName, 
                x.DateStart.ToString(), 
                x.DateEnd.ToString(),
                x.InterestRate.ToString(), 
                x.Money.ToString()
            }).ToArray());

            return package.GetAsByteArray();
        }

        private byte[] CreateExcel(IEnumerable<CashDTO> Cashes)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("Cashes");

            CreateHeader("Id", "User name", "Cash");
            Apply(Cashes.Select(x => new string[] {
                x.Id.ToString(), 
                x.UserName, 
                x.Money.ToString() 
            }).ToArray());

            return package.GetAsByteArray();
        }

        private byte[] CreateExcel(IEnumerable<ContractDTO> contracts)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("Contracts");

            CreateHeader("Id", "User name", "Other person", "Receipt date", "InterestRate", "Money credit", "Returned or not returned", "Returned date","Returned money", "Type contract");
            Apply(contracts.Select(x => new string[]{ 
                x.Id.ToString(), 
                x.UserName,
                x.OtherPerson,
                x.ReceiptDate.ToString(),
                x.InterestRate.ToString(),
                x.MoneyCredit.ToString(), 
                x.Returned ? "returned" : "Did not return",
                x.ReturnedDate is null ? "" : x.ReturnedDate.Value.ToString(),
                x.ReturnedMoney is null ? "" : x.ReturnedMoney.ToString(),
                x.TypeContract.ToString() 
            }).ToArray());

            return package.GetAsByteArray();
        }

        private byte[] CreateExcel(IEnumerable<ExpenseDTO> expenses)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("Expenses");

            CreateHeader("Id", "User name", "Category", "Sub category", "Expenditure date", "Money spent", "Purpose spending");
            Apply(expenses.Select(x => new string[]{
                x.Id.ToString(),
                x.UserName,
                x.Category,
                x.SubCategory,
                x.ExpenditureDate.ToString(),
                x.MoneySpent.ToString(),
                x.PurposeSpending
            }).ToArray());

            return package.GetAsByteArray();
        }

        private byte[] CreateExcel(IEnumerable<IncomeDTO> incomes)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("Incomes");

            CreateHeader("Id", "User name", "Money received", "Receipt date", "Type activity");
            Apply(incomes.Select(x => new string[]{
                x.Id.ToString(),
                x.UserName,
                x.MoneyReceived.ToString(),
                x.ReceiptDate.ToString(),
                x.TypeActivity
            }).ToArray());

            return package.GetAsByteArray();
        }

        private byte[] CreateExcel(IEnumerable<InvestAccountDTO> InvestAccounts)
        {
            var package = new ExcelPackage();

            _sheet = package.Workbook.Worksheets.Add("InvestAccounts");

            CreateHeader("Id", "User name", "Date start", "Date end", "Interest rate", "Price of the asset");
            Apply(InvestAccounts.Select(x => new string[]{
                x.Id.ToString(),
                x.UserName,
                x.DateStart.ToString(),
                x.DateEnd.ToString(),
                x.InterestRate.ToString(),
                x.Money.ToString()
            }).ToArray());

            return package.GetAsByteArray();
        }

        private void Apply(string[][] arrayValues)
        {
            for (int i = 0; i < arrayValues.Length; i++)
            {
                var values = arrayValues[i];
                for (int x = 0; x < values.Length; x++)
                {
                    _sheet.Cells[$"{_alpha[x]}{i+2}"].Value = values[x];
                }
            }
        }

        private void CreateHeader(params string[] nameColumns)
        {
            for (int i = 0; i < nameColumns.Length; i++)
            {
                _sheet.Cells[$"{_alpha[i]}{1}"].Value = nameColumns[i];
            }
        }
    }
}
