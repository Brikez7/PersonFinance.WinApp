using Newtonsoft.Json;
using PersonFinance.WinApp.PersonFinanceModels;
using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.Response;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PersonFinance.WinApp.ClientsWebAPI
{
    public static class PersonFinanceClientAPI<ModelDTO, RequestNew>
        where ModelDTO : BaseDTO
        where RequestNew : BaseRequestNew
    {
        private const string UrlAPI = "https://localhost:7214/PersonFinance/v1";

        private const string ControllerFinance = $"{UrlAPI}/Cash";
        private const string ControllerContract = $"{UrlAPI}/Contract";
        private const string ControllerExpense = $"{UrlAPI}/Expense";
        private const string ControllerIncome = $"{UrlAPI}/Income";
        private const string ControllerBankingAccount = $"{UrlAPI}/BankingAccount";
        private const string ControllerInvestAccount = $"{UrlAPI}/InvestAccount";

        private static HttpClient HttpClient { get; } = new HttpClient();

        private static string GetUrlController()
            => typeof(ModelDTO) switch
            {
                var _ when typeof(ModelDTO) == typeof(CashDTO) => ControllerFinance,
                var _ when typeof(ModelDTO) == typeof(ContractDTO) => ControllerContract,
                var _ when typeof(ModelDTO) == typeof(ExpenseDTO) => ControllerExpense,
                var _ when typeof(ModelDTO) == typeof(IncomeDTO) => ControllerIncome,
                var _ when typeof(ModelDTO) == typeof(InvestAccountDTO) => ControllerInvestAccount,
                var _ when typeof(ModelDTO) == typeof(BankingAccountDTO) => ControllerBankingAccount,
                _ => throw new NotImplementedException("this type not supposed"),
            };
        private static Guid GetIdController(ModelDTO model)
            => typeof(ModelDTO) switch
            {
                var _ when typeof(ModelDTO) == typeof(CashDTO) => (model as CashDTO)!.Id,
                var _ when typeof(ModelDTO) == typeof(ContractDTO) => (model as ContractDTO)!.Id,
                var _ when typeof(ModelDTO) == typeof(ExpenseDTO) => (model as ExpenseDTO)!.Id,
                var _ when typeof(ModelDTO) == typeof(IncomeDTO) => (model as IncomeDTO)!.Id,
                var _ when typeof(ModelDTO) == typeof(InvestAccountDTO) => (model as InvestAccountDTO)!.Id,
                var _ when typeof(ModelDTO) == typeof(BankingAccountDTO) => (model as BankingAccountDTO)!.Id,
                _ => throw new NotImplementedException("this type not supposed"),
            };
        public static async Task<StandardResponse<bool>> DeleteAsync(ModelDTO model, CancellationToken cancellationToken)
        {
            Guid modelId = GetIdController(model);
            var httpResponseMessage = await HttpClient.DeleteAsync($"{GetUrlController()}/Delete/{modelId}", cancellationToken);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            var response =  JsonConvert.DeserializeObject<StandardResponse<bool>>(responseMessage);

            return response!;
        }

        public static async Task<StandardResponse<ModelDTO[]>> GetAsync(CancellationToken cancellationToken)
        {
            var httpResponseMessage = await HttpClient.GetAsync($"{GetUrlController()}/GetAll", cancellationToken);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<StandardResponse<ModelDTO[]>>(responseMessage);

            return response!;
        }

        public static async Task<StandardResponse<ModelDTO>> InsertAsync(RequestNew requestNew, CancellationToken cancellationToken)
        {
            var contentRequestNew = JsonContent.Create(requestNew);

            var httpResponseMessage = await HttpClient.PostAsync($"{GetUrlController()}/Add", contentRequestNew, cancellationToken);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<StandardResponse<ModelDTO>>(responseMessage);

            return response!;
        }

        public static async Task<StandardResponse<ModelDTO>> UpdateAsync(ModelDTO modelDTO, CancellationToken cancellationToken)
        {
            var contentModelDTO = JsonContent.Create(modelDTO);

            var httpResponseMessage = await HttpClient.PutAsync($"{GetUrlController()}/Update", contentModelDTO, cancellationToken);

            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
            var response = JsonConvert.DeserializeObject<StandardResponse<ModelDTO>>(responseMessage);

            return response!;
        }
    }
}
