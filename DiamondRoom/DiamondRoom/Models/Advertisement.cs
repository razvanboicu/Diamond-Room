using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models
{
    public class Advertisement
    {
        public int idRoom { get; set; }
        public int discount { get; set; }
        public string roomType { get; set; }
        public string info { get; set; }
    }
}
