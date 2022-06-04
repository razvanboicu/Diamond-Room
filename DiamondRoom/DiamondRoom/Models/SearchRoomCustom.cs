using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models
{
    public class SearchRoomCustom
    {
        public int _roomID { get; set; }
        public string _roomType { get; set; }
        public int _price { get; set; }
        public bool _hasOffer { get; set; }
    }
}
