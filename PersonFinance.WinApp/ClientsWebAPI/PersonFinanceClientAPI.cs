using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.ContractDTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.ExpenseDTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.FinanceDTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.IncomeDTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.MoneyAccountDTOs;
using PersonFinance.WinApp.PersonFinanceModels.DTOs.PersonDTOs;
using PersonFinance.WinApp.PersonFinanceModels.Response;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PersonFinance.WinApp.ClientsWebAPI
{
    public static class PersonFinanceClientAPI<ModelDTO, RequestNew>
        where ModelDTO : BaseDTO
        where RequestNew : BaseRequestNew
    {
        private const string UrlAPI = "PersonFinance/v1";

        private const string ControllerPerson = $"{UrlAPI}/Person";
        private const string ControllerFinance = $"{UrlAPI}/Finance";
        private const string ControllerContract = $"{UrlAPI}/Contract";
        private const string ControllerExpense = $"{UrlAPI}/Expense";
        private const string ControllerIncome = $"{UrlAPI}/Income";
        private const string ControllerMoneyAccount = $"{UrlAPI}/MoneyAccount";
        private static HttpClient HttpClient { get; } = new HttpClient();

        private static string GetUrlController()
            => typeof(ModelDTO) switch
        {
            var _ when typeof(ModelDTO) == typeof(PersonDTO) => ControllerPerson,
            var _ when typeof(ModelDTO) == typeof(FinanceDTO) => ControllerFinance,
            var _ when typeof(ModelDTO) == typeof(ContractDTO) => ControllerContract,
            var _ when typeof(ModelDTO) == typeof(ExpenseDTO) => ControllerExpense,
            var _ when typeof(ModelDTO) == typeof(IncomeDTO) => ControllerIncome,
            var _ when typeof(ModelDTO) == typeof(MoneyAccountDTO) => ControllerMoneyAccount,
            _ => throw new NotImplementedException("this type not suposed"),
        };

        public static async Task<StandardResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await HttpClient.DeleteAsync($"{GetUrlController()}/Delete/{id}", cancellationToken);

            var content = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
            var response = await JsonSerializer.DeserializeAsync<StandardResponse<bool>>(content, cancellationToken: cancellationToken);
            
            return response!;
        }

        public static async Task<StandardResponse<ModelDTO[]>> GetAsync(CancellationToken cancellationToken)
        {
            var httpResponseMessage = await HttpClient.GetAsync($"{GetUrlController()}/GetAll", cancellationToken);

            var content = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
            var response = await JsonSerializer.DeserializeAsync<StandardResponse<ModelDTO[]>>(content, cancellationToken: cancellationToken);

            return response!;
        }

        public static async Task<StandardResponse<ModelDTO>> InsertAsync(RequestNew requestNew, CancellationToken cancellationToken)
        {
            var contentRequestNew = JsonContent.Create(requestNew);

            var httpResponseMessage = await HttpClient.PostAsync($"{GetUrlController()}/Add", contentRequestNew, cancellationToken);

            var content = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
            var response = await JsonSerializer.DeserializeAsync<StandardResponse<ModelDTO>>(content, cancellationToken: cancellationToken);

            return response!;
        }

        public static async Task<StandardResponse<ModelDTO>> UpdateAsync(ModelDTO modelDTO, CancellationToken cancellationToken)
        {
            var contentModelDTO = JsonContent.Create(modelDTO);

            var httpResponseMessage = await HttpClient.PostAsync($"{GetUrlController()}/Update", contentModelDTO, cancellationToken);

            var content = await httpResponseMessage.Content.ReadAsStreamAsync(cancellationToken);
            var response = await JsonSerializer.DeserializeAsync<StandardResponse<ModelDTO>>(content, cancellationToken: cancellationToken);

            return response!;
        }
    }
}
