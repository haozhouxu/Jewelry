using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.Helper
{
    public class GlobalBindingHelper
    {
        //用户名
        public static string Name { get; set; }
        //类别
        public static string JewelryType = "珠宝";
        public static string OwnType = "归属人";
        public static string ColorType = "颜色";
        //导航页
        //public static string GotoPage = "全部";
        //每页显示的条数
        public static int PageSize = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["pagesize"]);

    }
}
