using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract
{
    public class EstateHelperEnums
    {
        public enum EstateHelperRoles
        {
            GeneralAdmin, Admin, User
        }
    }

    public class RoleNames
    {
        public const string GeneralAdmin = nameof(EstateHelperEnums.EstateHelperRoles.GeneralAdmin);
        public const string Admin = nameof(EstateHelperEnums.EstateHelperRoles.Admin);
        public const string User = nameof(EstateHelperEnums.EstateHelperRoles.User);
    }

    public enum ProductUnitEnum
    {
        SquareMeter, Plot
    }

}
