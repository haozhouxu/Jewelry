using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;

namespace First
{
    public class ParameterObject : DependencyObject, INotifyPropertyChanged
    {
        public ParameterObject() { }

        public object Item
        {
            get { return (object)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(object), typeof(ParameterObject), new FrameworkPropertyMetadata(null, OnItemChanged));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == System.Windows.DependencyProperty.UnsetValue)
            {
                d.SetValue(ParameterObject.ItemProperty, null);
            }
        }



        public Type ItemType
        {
            get { return (Type)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(Type), typeof(ParameterObject), new PropertyMetadata(null));



        #region 实现INotifypropertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }

    public delegate void ParameterObjectCollectionChanged(ParameterObjectCollection parameters);



    public class ParameterObjectCollection : Collection<object>, IList, ICollection, IEnumerable
    {
        private bool _isReadOnly;
        private ParameterObjectCollectionChanged _parametersChanged;

        public ParameterObjectCollection(ParameterObjectCollectionChanged parametersChanged)
        {
            this._parametersChanged = parametersChanged;
        }

        private void CheckReadOnly()
        {
            if (this.IsReadOnly)
            {
                throw new Exception("Not applyed!");
            }
        }

        internal void ClearInternal()
        {
            base.ClearItems();
        }

        protected override void ClearItems()
        {
            this.CheckReadOnly();
            base.ClearItems();
            this.OnCollectionChanged();
        }

        protected override void InsertItem(int index, object value)
        {
            this.CheckReadOnly();
            base.InsertItem(index, value);
            this.OnCollectionChanged();
        }

        private void OnCollectionChanged()
        {
            this._parametersChanged(this);
        }

        protected override void RemoveItem(int index)
        {
            this.CheckReadOnly();
            base.RemoveItem(index);
            this.OnCollectionChanged();
        }

        protected override void SetItem(int index, object value)
        {
            this.CheckReadOnly();
            base.SetItem(index, value);
            this.OnCollectionChanged();
        }

        internal void SetReadOnly(bool isReadOnly)
        {
            this.IsReadOnly = isReadOnly;
        }

        // Properties
        protected bool IsFixedSize
        {
            get
            {
                return this.IsReadOnly;
            }
        }

        protected virtual bool IsReadOnly
        {
            get
            {
                return this._isReadOnly;
            }
            set
            {
                this._isReadOnly = value;
            }
        }

        bool IList.IsFixedSize
        {
            get
            {
                return this.IsFixedSize;
            }
        }

        bool IList.IsReadOnly
        {
            get
            {
                return this.IsReadOnly;
            }
        }

    }

}
