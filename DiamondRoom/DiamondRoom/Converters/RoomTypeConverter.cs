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
    public class RoomTypeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                 
                try
                {
                    int numVal = Int32.Parse(values[1].ToString());
                    //Console.WriteLine(numVal);
                    return new Room_type()
                    {

                        type = values[0].ToString(),
                        price = (float)numVal
                    };
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
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
