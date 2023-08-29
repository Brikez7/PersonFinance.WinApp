namespace PersonFinance.WinApp.PersonFinanceModels.Response
{
    public abstract class BaseResponse<T>
    {
        public virtual T? Data { get; set; }
        public virtual ServiceCode ServiceCode { get; set; }
        public virtual string? Message { get; set; } 
    }
}