namespace PersonFinance.WinApp.PersonFinanceModels.Response
{
    public class StandardResponse<T> : BaseResponse<T>
    {
        public override string? Message { get; set; }
        public override ServiceCode ServiceCode { get; set; } = ServiceCode.Ok;
        public override T? Data { get; set; }

        public StandardResponse(T? data)
        {
            Data = data;
        }

        public StandardResponse(T? data, ServiceCode serviceCode, string? message = null)
        {
            Message = message;
            ServiceCode = serviceCode;
            Data = data;
        }
    }
}