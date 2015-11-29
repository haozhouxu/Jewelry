using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ManagementSystem
{
    public static class Tools
    {
        public static List<SectionTitleCollection> LoadCatalog(XElement rootXE)
        {
            //doc1.Sects.Clear();
            Dictionary<int, SectionTitle> cacheDic = new Dictionary<int, SectionTitle>();
            cacheDic[0] = new SectionTitle();
            if (rootXE != null)
            {
                foreach (var e1 in rootXE.Elements())
                {
                    List<string> l1 = e1.Attributes().Select(a1 => a1.Name.LocalName).ToList<string>();
                    int p = (int)e1.Attribute("porder");
                    int o = (int)e1.Attribute("order");
                    string id = (string)e1.Attribute("id");
                    SectionTitle st1 = new SectionTitle()
                    {
                        OrderFormat = (string)e1.Attribute("orderFormat"),
                        OrderType = (string)e1.Attribute("orderType"),
                        TitleText = e1.Value,
                        ID = id,
                        ModuleID = id,
                        NavID = (string)e1.Attribute("nav"),
                        Code = (string)e1.Attribute("aid"),
                        Tip = (string)e1.Attribute("tip"),
                        Expand = (bool?)e1.Attribute("expand"),
                        Selected = (bool?)e1.Attribute("isSelected")
                    };
                    if (l1.Contains("isCustom"))
                    {
                        st1.IsCustom = (bool)e1.Attribute("isCustom");
                        st1.SectionElementString = (string)e1.Attribute("sectElement");
                    }
                    if (l1.Contains("isVisible"))
                    {
                        st1.IsVisiable = (bool)e1.Attribute("isVisible");
                    }
                    if (l1.Contains("ShowState"))
                    {
                        st1.ShowState = (SectionTitleShowState)Enum.Parse(typeof(SectionTitleShowState), (string)e1.Attribute("ShowState"));
                    }
                    if (cacheDic.ContainsKey(p))
                    {
                        cacheDic[p].Children.Add(st1);
                        //sec1.Parent = cacheDic[p];
                    }
                    cacheDic[p + 1] = st1;
                    // doc1.Sects[id] = st1;
                }

                List<SectionTitleCollection> docTitles = new List<SectionTitleCollection>();

                docTitles.AddRange(from c1 in cacheDic[0].Children
                                   select new SectionTitleCollection() { c1 });
                return docTitles;
            }
            return null;
        }

    }
}
