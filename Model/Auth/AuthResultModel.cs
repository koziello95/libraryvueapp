using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libraryVueApp.Model.Auth
{
    public class AuthResultModel
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
