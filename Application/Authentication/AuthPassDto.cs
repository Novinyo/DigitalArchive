using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class AuthPassDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string MatchPassword { get; set; }
    }
}