using PersonFinance.WinApp.PersonFinanceModels.DTOs;
using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.PersonDTOs
{
    public class RequestNewPerson : BaseRequestNew
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public RequestNewPerson(Guid personId, string name)
        {
            PersonId = personId;
            Name = name;
        }
    }
}
