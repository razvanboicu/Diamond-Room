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
                string answ;
                if (values[1].ToString().Equals("FALSE"))
                    answ = "FALSE";
                else answ = "TRUE";

                   
                return new Extra_features()
                {
                    price = 0, //trebuie sa ma gandesc cum convertesc string la int
                    deleted = Boolean.Parse(answ),
                    service = values[2].ToString()
                };
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
