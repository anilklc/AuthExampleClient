using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.DTOs.Order
{
    public class Order
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
