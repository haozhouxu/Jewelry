using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace First.Model
{
    //class TypeEntity:ObservableCollection<string>
    //{
    //    public TypeEntity()
    //    {
    //        this.Add("耳坠");
    //        this.Add("挂件");
    //        this.Add("戒指");
    //        this.Add("手链");
    //        this.Add("手镯");
    //    }
    //}

    public class Type
    {
        public string type { set; get; }
    }

    public class TypeEntity : ObservableCollection<Type>
    {
        public TypeEntity()
        {
            this.Add(new Type { type = "耳坠" });
            this.Add(new Type { type = "挂件" });
            this.Add(new Type { type = "戒指" });
            this.Add(new Type { type = "手链" });
            this.Add(new Type { type = "手镯" });
        }
    }
}
