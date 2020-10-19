using System;
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

        public override string ToString()
        {
            return $"(T {T}    Value {Value})";
        }
    }
}
