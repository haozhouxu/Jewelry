using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace ManagementSystem
{
    public class MultiParameterConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                string para = (string)parameter;
                if (!string.IsNullOrEmpty(para))
                {
                    Dictionary<string, object> paraDic = new Dictionary<string, object>() { { "context", values[0] }, { "pageID", values[1] } };
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
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiObjectDataConvertor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null)
            {
                string para = (string)parameter;
                if (!string.IsNullOrEmpty(para))
                {
                    Dictionary<string, object> paraDic = new Dictionary<string, object>();
                    int i = 1;
                    foreach (object obj in values)
                    {
                        paraDic.Add("context"+i,obj);
                        i++;
                    }
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
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
