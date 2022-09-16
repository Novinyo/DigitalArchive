using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class UserDto
    {
        public UserAuthDto User { get; set; }
        public string Access_Token { get; set; }
    }
    public class UserAuthDto
    {
        public string Uuid { get; set; }
        public string From { get; set; }
        public string[] Roles { get; set; }
        public UserDetails Data { get; set; }

    }

    public class UserDetails
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
       // public string Username { get; set; }
        public string PhotoURL { get; set; }
        public Settings Settings { get; set; }
        public string[] Shortcuts { get; set; }
    }

    public class Settings
    {
        public object Layout { get; set; }
        public object Theme { get; set; }
    }

    public class Layout
    {
    }

    public class Theme
    {
    }
}