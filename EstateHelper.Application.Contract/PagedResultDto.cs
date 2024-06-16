using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract
{
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public T? Data { get; set; }

    }
}
