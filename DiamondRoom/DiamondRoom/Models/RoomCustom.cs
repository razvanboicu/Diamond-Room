using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models
{
    public class RoomCustom
    {
        public int id { get; set; }
        public bool available { get; set; }
        public string type { get; set; }
        public int price { get; set; }
    }
}
