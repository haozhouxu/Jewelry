using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Data;
using System.Reflection;

namespace First
{
    public class ActionCenter
    {
    }

    public class CallSetProperty : TargetedTriggerAction<DependencyObject>
    {

        public DependencyProperty TargetPorperty
        {
            get { return (DependencyProperty)GetValue(TargetPorpertyProperty); }
            set { SetValue(TargetPorpertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetPorperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetPorpertyProperty =
            DependencyProperty.Register("TargetPorperty", typeof(DependencyProperty), typeof(CallSetProperty), new PropertyMetadata(null));



        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(CallSetProperty), new PropertyMetadata(null));



        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Path.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(CallSetProperty), new PropertyMetadata(null));



        public bool IsDirectSet
        {
            get { return (bool)GetValue(IsDirectSetProperty); }
            set { SetValue(IsDirectSetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDirectSet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDirectSetProperty =
            DependencyProperty.Register("IsDirectSet", typeof(bool), typeof(CallSetProperty), new PropertyMetadata(false));




        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(CallSetProperty), new PropertyMetadata(null));



        protected override void Invoke(object parameter)
        {
            if (Target != null && TargetPorperty != null)
            {

                if (Value != null)
                    Target.SetValue(TargetPorperty, Value);
                else
                {
                    Binding b1 = new Binding(this.Path);
                    b1.Source = Source;
                    EvalHelper vh = new EvalHelper();
                    BindingOperations.SetBinding(vh, EvalHelper.ValueProperty, b1);
                    Target.SetValue(TargetPorperty, vh.Value);
                }
            }

        }
    }

    class EvalHelper : DependencyObject
    {
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(EvalHelper));
    }


}
