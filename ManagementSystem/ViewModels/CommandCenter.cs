using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementSystem.Views;

namespace ManagementSystem.ViewModels
{
    public class CC
    {
        public static void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Dictionary<string, object> paraDic = e.Parameter as Dictionary<string, object>;
                if (paraDic == null)
                {
                    return;
                }
                string type = (string)paraDic["type"];
                switch (type)
                {
                    case "addNew":
                        CC.AddNew(paraDic);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void AddNew(Dictionary<string ,object > paraDic)
        {
            Add an = new Add();
            an.Show();
        }
    }
}
