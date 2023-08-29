using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs
{
    public class RequestNewCash : BaseRequestNew
    {
        public string UserName { get; set; } = null!;
        public Money Money { get; set; } = null!;

        public RequestNewCash(string userName, Money money)
        {
            UserName = userName;
            Money = money;
        }
    }
}
