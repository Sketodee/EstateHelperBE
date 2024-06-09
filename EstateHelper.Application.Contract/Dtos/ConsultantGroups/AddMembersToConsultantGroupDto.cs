using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.ConsultantGroups
{
    public class AddMembersToConsultantGroupDto
    {
        public bool addMembers {  get; set; }   
        public string Id { get; set; }
        public List<string> MembersId { get; set; }   
    }
}
