using System;
using System.Globalization;
using System.Numerics;

namespace Laba1
{
    struct DataItem
    {
        public float T { get; }
        public Vector3 Value { get; }

        public DataItem(float t, Vector3 value)
        {
            T = t;
            Value = value;
        }

        public string ToString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string res = ToString() + "\n vector length " + Value.Length()+ '\n';
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return res;
        }

        public override string ToString()
        {
            return $" T {T}    Value {Value}";
        }
    }
}
