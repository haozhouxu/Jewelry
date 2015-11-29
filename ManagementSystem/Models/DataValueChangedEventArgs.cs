using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagementSystem
{
    public delegate void DataValueChangedEventHandler(object sender, DataValueChangedEventArgs e);

    public class DataValueChangedEventArgs : EventArgs
    {
        public DataValueChangedEventArgs()
        {
            this.TupleContext = new Dictionary<string, int>();
        }

        public string Path { get; set; }

        public string DataID { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }    

        public DateTime ChangedTime { get; set; }

        public string TupleID { get; set; }

        public Dictionary<string, int> TupleContext { get; set; }
    }
}
