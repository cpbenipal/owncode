using Abp.AutoMapper;
using PanelMasterMVC5Separate.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Job.Dto
{
    [AutoMapTo(typeof(Client))]
    public class CreateClientInput
    {             
        public string Name { get; set; }           
        public string Surname { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string CommunicationType { get; set; }
        public bool ContactAfterService { get; set; }
    }
}
