using System;

namespace PersonFinance.WinApp.PersonFinanceModels.DTOs.PersonDTOs
{
    public class PersonDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PersonDTO(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
