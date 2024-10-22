using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.DTOs.AuthorizeMenu
{
    public class AssignRole
    {
        public string[] Roles { get; set; }
        public string Menu { get; set; }
        public string Code { get; set; }
    }
}
