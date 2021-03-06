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
    public class ExtraFeatureConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                try
                {
                    bool answ;
                    if (values[1].ToString().Equals("False"))
                        answ = false;
                    else answ = true;
                    int numVal = Int32.Parse(values[0].ToString());
                    //Console.WriteLine(numVal);
                    Console.WriteLine("s-a intrat in converter");
                    return new Extra_features()
                    {
                        price = (int) numVal, //trebuie sa ma gandesc cum convertesc string la int
                        deleted = answ,
                        service = values[2].ToString()
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
