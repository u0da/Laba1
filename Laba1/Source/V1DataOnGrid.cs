using System;
using System.Collections.Generic;
using System.Numerics;

namespace Laba1
{
    class V1DataOnGrid : V1Data
    {
        public Grid Grid { get; }
        public Vector3[] Values { get; }

        public V1DataOnGrid(string info,DateTime date, Grid grid) : base(info, date )
        {
            Grid = grid;
            Values = new Vector3[grid.Number];
        }

        public void InitRandom(float minValue, float maxValue)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = new Vector3();
                Random rand = new Random();
                Values[i].X = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
                Values[i].Y = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
                Values[i].Z = (float)(minValue + (maxValue - minValue) * rand.NextDouble());
            }
        }

        public static explicit operator V1DataCollection(V1DataOnGrid data)
        {
            V1DataCollection newData = new V1DataCollection(data.Info, data.Date);
            for (int i = 0; i < data.Values.Length; i++)
            {
                DataItem item = new DataItem(data.Grid.GetTime(i), data.Values[i]);
                newData.Values.Add(item);
            }
            return newData;
        }

        public override float[] NearZero(float eps)
        {
            List<float> result = new List<float>();
            for (int i = 0; i < Values.Length; i++)
            {
                Vector3 vector = Values[i];
                if (vector.Length() < eps)
                    result.Add(Grid.GetTime(i));
            }
            return result.ToArray();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {base.ToString()}, {Grid}";
        }

        public override string ToLongString()
        {
            string newStr = "\n";
            for (int i = 0; i < Values.Length; i++)
            {
                newStr += $"Time {Grid.GetTime(i)} \t Values {Values[i]}\n";
            }
            return $"{ToString()} \t {newStr}";
        }
    }
}
