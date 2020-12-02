using System;
using System.Collections.Generic;
using System.Collections;

namespace Laba1
{
    abstract class V1Data : IEnumerable<DataItem>
    {
        public string Info { get; set; }
        public DateTime Date { get; set; }

        public V1Data() {}
        public V1Data(string info, DateTime date)
        {
            Info = info;
            Date = date;
        }

        public abstract float[] NearZero(float eps);
        public abstract string ToLongString();
        public override string ToString()
        {
            return $"Date {Date} \n Info {Info}";
        }

        public abstract string ToLongString(string format);
        public abstract IEnumerator<DataItem> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
