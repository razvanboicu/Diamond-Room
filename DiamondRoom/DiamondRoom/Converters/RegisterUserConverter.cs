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
    public class RegisterUserConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null &&
                values[4] != null && values[5] != null && values[6] != null && values[7] != null &&
                values[8] != null && values[9] != null)
            {
                return new UserCustom()
                {
                    _firstName = values[0].ToString(),
                    _lastName = values[1].ToString(),
                    _phoneNr1 = values[2].ToString(),
                    _phoneNr2 = values[3].ToString(),
                    _country = values[4].ToString(),
                    _city = values[5].ToString(),
                    _street = values[6].ToString(),
                    _email = values[7].ToString(),
                    _username = values[8].ToString(),
                    _password = values[9].ToString()
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
