using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.Client
{
    public class GetClientDto
    {
        public string Id { get; set; } 
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int ConsultantId { get; set; }
        public int Points { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName { get; set; }
    }
}
