using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.DTOs.AuthorizeMenu
{
    public class ActionItem
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definiton { get; set; }
        public string Code { get; set; }
    }

}
