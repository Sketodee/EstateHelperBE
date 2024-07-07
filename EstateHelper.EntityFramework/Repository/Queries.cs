using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework.Repository
{
    public class Queries
    {
        public static string GetMemebersOfConsultantGroup()
        {
            return $@"
                select Id, Surname, FirstName, PhoneNumber, Email, Link, isActive from AspNetUsers where Id in ('f4e29715-bad9-4923-b001-351942bda373','82ff3f87-4117-42f8-85c1-b2cc3f1151a5')
            ";
        }
    }
}
