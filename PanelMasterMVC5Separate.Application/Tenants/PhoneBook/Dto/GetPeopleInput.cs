
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PanelMasterMVC5Separate.PhoneBook;


namespace PanelMasterMVC5Separate.Tenants.PhoneBook.Dto
{
    public class GetPeopleInput
    {
        public string Filter { get; set; }
    }

    [AutoMapFrom(typeof(Person))]
    public class PersonListDto : FullAuditedEntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }
    }
}
