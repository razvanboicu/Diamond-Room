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
    public class OfferCustomConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null
                && values[3] != null && values[4] != null && values[5] != null)
            {
                try
                {
                    int numVal = Int32.Parse(values[0].ToString());
                    //int intPrice = Int32.Parse(values[2].ToString());
                    int discount = Int32.Parse(values[3].ToString());
    
                    return new OfferCustom()
                    {

                        id = numVal,
                        type = values[1].ToString(),
                        price = -1,
                        discount = discount,
                        available = false,
                        obs = values[5].ToString(),
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("GRESITTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
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
