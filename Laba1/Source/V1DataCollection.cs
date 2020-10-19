using System;
using System.Collections.Generic;
using System.Numerics;

namespace Laba1
{
    class V1DataCollection : V1Data
    {
        public List<DataItem> Values { get; } = new List<DataItem>();

        public V1DataCollection(string info, DateTime date)
              : base(info, date) { }

        public void InitRandom(int nItems, float tmin, float tmax, float minValue, float maxValue)
        {
            for (int i = 0; i < nItems; i++)
            {
                Random rand = new Random();

                float x = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
                float y = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
                float z = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
                float time = (float)(tmin + (tmax - tmin) * rand.NextDouble());
                DataItem item = new DataItem(time, new Vector3(x, y, z));
                Values.Add(item);
            }
        }

        public override float[] NearZero(float eps)
        {
            List<float> result = new List<float>();
            foreach (DataItem item in Values)
            {
                if (item.Value.Length() < eps)
                    result.Add(item.T);
            }
            return result.ToArray();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {base.ToString()}, {Values.Count}";
        }

        public override string ToLongString()
        {
            string newStr = "\n";
            foreach (DataItem item in Values)
            {
                newStr += $"T {item.T} \t Value  {item.Value}\n";
            }
            return $"{ToString()} \t {newStr}";
        }
    }
}