using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace ManagementSystem
{

    public class SectionTitle : DependencyObject, INotifyPropertyChanged
    {
        string[] ChineseNums = new string[] { "", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        string[] ChineseCapNums = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        string GetChineseNumber(int order)
        {
            StringBuilder sb1 = new StringBuilder();
            int rem = 0;
            int quo = Math.DivRem(order, 10, out rem);
            if (quo > 0)
            {
                if (quo == 1)
                    sb1.Append("十");
                else
                    sb1.Append(ChineseNums[quo] + "十");
            }
            sb1.Append(ChineseNums[rem]);
            return sb1.ToString();
        }
        string GetChineseCapNumber(int order)
        {
            StringBuilder sb1 = new StringBuilder();
            int rem = 0;
            int quo = Math.DivRem(order, 10, out rem);
            if (quo > 0)
            {
                sb1.Append(ChineseNums[quo]);
            }
            sb1.Append(ChineseCapNums[rem]);
            return sb1.ToString();
        }

        public SectionTitle()
        {
            this.Children = new SectionTitleCollection(this);
            this.Selected = false;
            this.ID = string.Empty;
            this.NavID = string.Empty;
            this.Code = string.Empty;
            this.OrderFormat = string.Empty;
            this.Tip = string.Empty;
        }

        public SectionTitleCollection Children { get; private set; }

        private string _ordreType = string.Empty;
        /// <summary>
        /// 序号的类别，标识是阿拉伯数字，或者汉字等
        /// </summary>
        public string OrderType
        {
            get { return _ordreType; }
            set
            {
                if (_ordreType != value)
                {
                    _ordreType = value;
                    Notify("OrderType");
                    Notify("OrderText");
                    Notify("Title");
                }
            }
        }
        
        private int _order = 1;
        /// <summary>
        /// 序号
        /// </summary>
        public int Order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    Notify("Order");
                    Notify("OrderText");
                    Notify("TitleText");
                    Notify("Title");
                }
            }
        }

        public int ParentOrder
        {
            get
            {
                if (this.Parent == null || string.IsNullOrEmpty(this.Parent.TitleText))
                    return 0;
                return this.Parent.Order;
            }
        }

        private string _orderFmt = @"{0}";

        public string OrderFormat
        {
            get { return _orderFmt; }
            set
            {
                if (_orderFmt != value)
                {
                    _orderFmt = value;
                    Notify("OrderFormat");
                    Notify("OrderText");
                    Notify("Title");
                }
            }
        }

        /// <summary>
        /// 序号的完整表达
        /// </summary>
        public string OrderText
        {
            get
            {
                switch (OrderType)
                {
                    case "N":
                        return string.Format(this.OrderFormat, this.Order);
                    case "C":
                        return string.Format(this.OrderFormat, GetChineseNumber(this.Order));
                    case "C1":
                        return string.Format(this.OrderFormat, GetChineseCapNumber(this.Order));
                    default:
                        return string.Empty;
                }
            }
        }

        private string _titleText = string.Empty;
        /// <summary>
        /// 标题的文本
        /// </summary>
        public string TitleText
        {
            get { return _titleText; }
            set
            {
                _titleText = value;
                Notify("TitleText");
                Notify("Title");
            }
        }

        /// <summary>
        /// 带有序号的完整标题表达
        /// </summary>
        public string Title
        {
            get
            {
                return string.Format("{0}{1}", this.OrderText, this.TitleText);
            }
        }

        bool _isVisiable = true;
        /// <summary>
        /// 表明此节是否可见
        /// </summary>
        public bool IsVisiable
        {
            get { return _isVisiable; }
            set
            {
                if (_isVisiable != value)
                {
                    _isVisiable = value;
                    Notify("IsVisiable");
                    Notify(System.Windows.Data.Binding.IndexerName);
                }
            }
        }

        private bool _isCustom = false;
        /// <summary>
        /// 表明此节是否为用户自行添加的自定义章节
        /// </summary>
        public bool IsCustom
        {
            get { return _isCustom; }
            set
            {
                if (_isCustom != value)
                {
                    _isCustom = value;
                    Notify("IsCustom");
                }
            }
        }

        private SectionTitle _parent = null;
        public SectionTitle Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null)
                    {
                        this.StampStateChanged -= _parent.ChildStampStateChanged;
                    }
                    _parent = value;
                    if (_parent != null)
                    {
                        this.StampStateChanged += _parent.ChildStampStateChanged;
                    }
                    Notify("Parent");
                }
            }
        }

        public IEnumerable<SectionTitle> Parents
        {
            get
            {
                SectionTitle st1 = this.Parent;
                while (st1 != null)
                {
                    SectionTitle st2 = st1;
                    st1 = st1.Parent;
                    yield return st2;
                }
            }
        }

        private SectionTitleShowState _showState = SectionTitleShowState.Normal;
        public SectionTitleShowState ShowState
        {
            get { return _showState; }
            set
            {
                if (_showState != value)
                {
                    var old = _showState;
                    _showState = value;
                    if (_showState == SectionTitleShowState.NA)
                    {
                        foreach (var s1 in this.FindAll())
                        {
                            s1.ShowState = _showState;
                        }
                    }
                    else
                    {
                        var p1 = this.Parent;
                        while (p1 != null && p1.ShowState == SectionTitleShowState.NA)
                        {
                            p1.ShowState = SectionTitleShowState.Normal;
                            p1 = p1.Parent;
                        }
                    }
                    if (this.Parent != null)//增加判断是否非空,qamar 
                    {
                        this.Parent.Children.ResetOrder();
                    }

                    if (ShowStateChanged != null)
                        ShowStateChanged.Invoke(this, new DataValueChangedEventArgs()
                        {
                            ChangedTime = DateTime.Now,
                            OldValue = old,
                            NewValue = value
                        });
                    Notify("ShowState");
                }
            }
        }

        private SectionTitleStampState _stampState = SectionTitleStampState.None;
        public SectionTitleStampState StampState
        {
            get { return _stampState; }
            set
            {
                if (_stampState != value)
                {
                    var old = _stampState;
                    _stampState = value;
                    if (StampStateChanged != null)
                        StampStateChanged.Invoke(this, new DataValueChangedEventArgs()
                        {
                            ChangedTime = DateTime.Now,
                            DataID = "StampState",
                            OldValue = old,
                            NewValue = _stampState
                        });
                    Notify("StampState");
                }
            }
        }


        public SectionTitle Next
        {
            get
            {
                if (Parent != null)
                {
                    int idx = Parent.Children.IndexOf(this);
                    if (idx > -1 && idx < Parent.Children.Count - 1)
                    {
                        return Parent.Children[idx + 1];
                    }
                }
                return null;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify(string pName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(pName));
        }

        public event DataValueChangedEventHandler ShowStateChanged;
        public event DataValueChangedEventHandler StampStateChanged;


        public void ChildStampStateChanged(object sender, DataValueChangedEventArgs de)
        {
            //TODO:在子节点状态变化时更新父节点的状态
            this.StampState = (SectionTitleStampState)de.NewValue;
        }


        public void InsertAfterSelf(SectionTitle newST)
        {
            if (Parent != null)
            {
                int idx = Parent.Children.IndexOf(this);
                if (idx > -1)
                {
                    Parent.Children.Insert(idx + 1, newST);
                }
            }
        }

        public void InsertBeforeSelf(SectionTitle newST)
        {
            if (Parent != null)
            {
                int idx = Parent.Children.IndexOf(this);
                if (idx > -1)
                {
                    Parent.Children.Insert(idx, newST);
                }
            }
        }

        public object this[string type]
        {
            get
            {
                switch (type)
                {
                    case "v":
                        if (IsVisiable)
                            return Visibility.Visible;
                        else
                            return Visibility.Collapsed;
                    default:
                        return null;
                }
            }
        }


        public override string ToString()
        {
            return string.Format("Title={0}, Order={1}, Type={2}", Title, Order, OrderType);
        }


        public string ID { get; set; }
        public string ModuleID { get; set; }

        public string NavID { get; set; }

        public string Code { get; set; }
        public string Tip { get; set; }
        //public FrameworkElement SectionElement { get; set; }

        public string SectionElementString { get; set; }

        public string[] SectionDataIDS { get; set; }

        bool? _selected;
        public bool? Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    Notify("Selected");
                }
            }
        }

        bool? _isNavigated;
        public bool? IsNavigated 
        {
            get
            {
                return _isNavigated;
            }
            set
            {
                if (_isNavigated != value)
                {
                    _isNavigated = value;
                    Notify("IsNavigated");                  
                }
            }
        }

        bool? _expand;
        public bool? Expand
        {
            get
            {                
                return _expand;
            }
            set
            {
                if (_expand != value)
                {
                    _expand = value;
                    Notify("Expand");
                }
            }
        }

        public int Depth
        {
            get
            {
                int dp = 0;
                SectionTitle st1 = this.Parent;
                while (st1 != null)
                {
                    dp++;
                    st1 = st1.Parent;
                }
                return dp;
            }
        }

        public static SectionTitle CreateShallowCopy(SectionTitle orgialST)
        {
            SectionTitle st1 = new SectionTitle();
            st1.TitleText = orgialST.TitleText;
            st1.OrderFormat = orgialST.OrderFormat;
            st1.OrderType = orgialST.OrderType;
            return st1;
        }


        private void CollectST(List<SectionTitle> l1, SectionTitle st1)
        {
            l1.Add(st1);
            for (int i = 0; i < st1.Children.Count; i++)
            {
                CollectST(l1, st1.Children[i]);
            }
        }

        public List<SectionTitle> AllTitles
        {
            get
            {
                List<SectionTitle> l1 = new List<SectionTitle>();
                CollectST(l1, this);
                return l1;
            }
        }

        public IEnumerable<SectionTitle> FindAll()
        {
            yield return this;
            foreach (var sect1 in Children)
            {
                foreach (var c1 in sect1.FindAll())
                    yield return c1;
            }
        }

        public IEnumerable<SectionTitle> FindAll(SectionTitleShowState tState)
        {
            if ((this.ShowState & tState) == tState)
            {
                yield return this;
                foreach (var sect1 in Children)
                {
                    foreach (var c1 in sect1.FindAll(tState))
                        yield return c1;
                }
            }
        }

        public IEnumerable<SectionTitle> Find(string navID)
        {
            if (NavID == navID)
                yield return this;
            foreach (var sect1 in Children)
            {
                foreach (var c1 in sect1.Find(navID))
                {
                    yield return c1;
                }
            }
        }
    }


    public class SectionTitleCollection : ObservableCollection<SectionTitle>
    {

        public SectionTitleCollection()
            : base()
        {
            this.InitNumber = 1;
        }

        public SectionTitleCollection(SectionTitle ownerTitle)
            : base()
        {
            this.Owner = ownerTitle;
            this.InitNumber = 1;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (SectionTitle it1 in e.NewItems)
                {
                    it1.Parent = this.Owner;
                }
            }
            else
            {
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (SectionTitle it1 in e.OldItems)
                    {
                        it1.Parent = null;
                    }
                }
            }
            base.OnCollectionChanged(e);
            ResetOrder();
            this.OnPropertyChanged(new PropertyChangedEventArgs("ViewUsed"));
        }

        public SectionTitle Owner { get; set; }
        public string SectID { get; set; }
        public string XBRLID { get; set; }
        public int InitNumber { get; set; }

        public void ResetOrder()
        {
            int init = this.InitNumber;
            for (int i = 1; i <= this.Count; i++)
            {
                var sTitle = this[i - 1];
                if (sTitle.IsVisiable && sTitle.ShowState != SectionTitleShowState.NA)
                {
                    sTitle.Order = init;
                    init++;
                }
            }
        }



    }

    [Flags]
    public enum SectionTitleStampState : short
    {
        None = 0x00,
        StartFill = 0x01,
        EndFill = 0x02,
        ContainSearchResults = 0x10,
        ContainValidationError = 0x20
    };

    [Flags]
    public enum SectionTitleShowState : short
    {
        None = 0x00,
        Normal = 0x01,
        NA = 0x02
    }
}
