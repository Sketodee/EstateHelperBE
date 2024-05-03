﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Dtos.Login
{
    public class RefreshToken
    {
        public required string Token { get; set; }
        public DateTime Created { get; set; }    = DateTime.Now;
        public DateTime Expires { get; set;} 
    }
}
