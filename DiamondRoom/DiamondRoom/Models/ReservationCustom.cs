using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models
{
    public class ReservationCustom
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int roomNumber { get; set; }
        public string roomType { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public string status { get; set; }
        public string observations { get; set;  }
        public int total { get; set; }
        public int idReservation { get; set; }
    }
}
