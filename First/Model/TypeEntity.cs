using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace First.Model
{
    class TypeEntity:ObservableCollection<string>
    {
        public TypeEntity()
        {
            this.Add("耳坠");
            this.Add("挂件");
            this.Add("戒指");
            this.Add("手链");
            this.Add("手镯");
        }

        //public static ObservableCollection<string> GetExample()
        //{
        //    ObservableCollection<string> ob = new ObservableCollection<string>();
        //    ob.Add("耳坠");
        //    ob.Add("挂件");
        //    ob.Add("戒指");
        //    ob.Add("手链");
        //    ob.Add("手镯");
        //    return ob;
        //}
    }
}
