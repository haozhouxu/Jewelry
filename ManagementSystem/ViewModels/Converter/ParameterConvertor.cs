using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ManagementSystem.ViewModels
{
    public class ParameterConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string para = (string)parameter;
            if (!string.IsNullOrEmpty(para))
            {
                Dictionary<string, object> paraDic = new Dictionary<string, object>() { { "context", value } };
                MatchCollection mc = Regex.Matches(para, @"[^\|]+");
                foreach (Match m1 in mc)
                {
                    Match m2 = Regex.Match(m1.Value, @"(?<name>[^:]+):(?<val>[^:]+):(?<type>[^\|]+)");
                    if (m2.Success)
                    {
                        switch (m2.Groups["type"].Value)
                        {
                            case "i":
                                paraDic[m2.Groups["name"].Value] = int.Parse(m2.Groups["val"].Value);
                                break;
                            case "b":
                                paraDic[m2.Groups["name"].Value] = bool.Parse(m2.Groups["val"].Value);
                                break;
                            default:
                                paraDic[m2.Groups["name"].Value] = m2.Groups["val"].Value;
                                break;

                        }
                    }
                }
                return paraDic;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
