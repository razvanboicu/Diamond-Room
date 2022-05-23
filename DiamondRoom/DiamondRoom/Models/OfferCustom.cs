using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models
{
    public class OfferCustom
    {
        public int id { get; set; }
        public string type { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public bool available { get; set; }
        public string obs { get; set; }
    }
}
