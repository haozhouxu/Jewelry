using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Data;
using System.Reflection;
using System.Collections;

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

    public class CallInvokeObjectMethod : TargetedTriggerAction<object>
    {
        public CallInvokeObjectMethod()
            : base()
        {
            this.ParameterTypes = new List<string>();
            this._paras = new ParameterObjectCollection(new ParameterObjectCollectionChanged(OnParametersChanged));
        }

        private ParameterObjectCollection _paras = null;

        public Type DestType
        {
            get { return (Type)GetValue(DestTypeProperty); }
            set { SetValue(DestTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DestObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DestTypeProperty =
            DependencyProperty.Register("DestType", typeof(Type), typeof(CallInvokeObjectMethod), new UIPropertyMetadata());




        public string DestMethod
        {
            get { return (string)GetValue(DestMethodProperty); }
            set { SetValue(DestMethodProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DestMethod.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DestMethodProperty =
            DependencyProperty.Register("DestMethod", typeof(string), typeof(CallInvokeObjectMethod), new UIPropertyMetadata(""));




        public object[] MethodParameters
        {
            get { return (object[])GetValue(MethodParametersProperty); }
            set { SetValue(MethodParametersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MethodParameters.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MethodParametersProperty =
            DependencyProperty.Register("MethodParameters", typeof(object[]), typeof(CallInvokeObjectMethod), new UIPropertyMetadata());

        public IList Parameters
        {
            get { return _paras; }
        }

        //public ParameterObjectCollection Parameters
        //{
        //    get { return (ParameterObjectCollection)GetValue(ParametersProperty); }
        //    set { SetValue(ParametersProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Parameters.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ParametersProperty =
        //    DependencyProperty.Register("Parameters", typeof(ParameterObjectCollection), typeof(CallInvokeObjectMethod), new UIPropertyMetadata());




        public List<string> ParameterTypes
        {
            get { return (List<string>)GetValue(ParameterTypesProperty); }
            set { SetValue(ParameterTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ParameterTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParameterTypesProperty =
            DependencyProperty.Register("ParameterTypes", typeof(List<string>), typeof(CallInvokeObjectMethod), new UIPropertyMetadata());

        private void OnParametersChanged(ParameterObjectCollection sender)
        {

        }




        protected override void Invoke(object parameter)
        {
            if (DestType != null && !string.IsNullOrEmpty(DestMethod))
            {
                MethodInfo mInfo = null;
                if (ParameterTypes.Count > 0)
                {
                    var ts = from s1 in this.ParameterTypes
                             select Type.GetType(s1);
                    mInfo = DestType.GetMethod(DestMethod, ts.ToArray());
                    if (mInfo != null && Parameters.Count > 0)
                    {
                        mInfo.Invoke(TargetObject, _paras.ToArray());//为了界面绑定，改为list，为了兼容之前的代码，暂时保留MethodParameters的使用
                        return;
                    }
                }
                else
                {
                    if (Parameters.Count > 0)
                    {
                        Type[] ts = new Type[Parameters.Count];
                        object[] vs = new object[Parameters.Count];
                        for (int i = 0; i < Parameters.Count; i++)
                        {
                            ParameterObject po1 = Parameters[i] as ParameterObject;
                            if (po1 != null)
                            {
                                vs[i] = po1.Item;
                                if (po1.ItemType != null)
                                    ts[i] = po1.ItemType;
                                else
                                {
                                    if (po1.Item != null)
                                        ts[i] = po1.Item.GetType();
                                    else
                                        ts[i] = typeof(object);
                                }
                            }
                            else
                            {
                                vs[i] = Parameters[i];
                                ts[i] = Parameters[i].GetType();
                            }
                        }
                        mInfo = DestType.GetMethod(DestMethod, ts.ToArray());
                        if (mInfo != null)
                        {
                            mInfo.Invoke(TargetObject, vs);
                            return;
                        }
                    }
                    else
                        mInfo = DestType.GetMethod(DestMethod);
                }

                if (mInfo != null)
                {
                    object o1 = mInfo.Invoke(TargetObject, MethodParameters);
                }
            }
        }
    }

}
