using DiamondRoom.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DiamondRoom.Converters
{
    public class ReservationsCustomConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null &&
                values[4] != null && values[5] != null && values[6]!=null &&
                values[7] != null && values[8] !=null)
            {
                try
                {
                    int roomNr = Int32.Parse(values[2].ToString());
                    return new ReservationCustom()
                    {
                        firstName = values[0].ToString(),
                        lastName = values[1].ToString(),
                        roomNumber = roomNr,
                        roomType = values[3].ToString(),
                        dateFrom = DateTime.UtcNow,
                        dateTo = DateTime.UtcNow,
                        status = values[6].ToString(),
                        observations = values[7].ToString(),
                        total = 0
                    };
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
