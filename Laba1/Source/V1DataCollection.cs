using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Globalization;
/*
 *info
 *date dd.mm.yyyy
 *time i
 *point[i].x
 *point[i].y
 *point[i].z



test_data
22.12.2222
0,1
50,1
9,5
3,22
8,5
0,86
20,2
18,39

example for 2 points
 */
namespace Laba1
{
    class V1DataCollection : V1Data, IEnumerable<DataItem>
    {
        public List<DataItem> Values { get; set; } = new List<DataItem>();

        public V1DataCollection(string info, DateTime date)
              : base(info, date) { }

        public V1DataCollection(string filename)
        {
            static float ReadSingle(StreamReader sReader) // пишем свое,
                                                          // потому что из документации почему-то не работает:(((
            {
                float result;
                if (!sReader.EndOfStream)
                    result = Convert.ToSingle(sReader.ReadLine());
                else
                    throw new Exception("No pos");
                return result;
            }
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

            Values = new List<DataItem>();
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader sReader = new StreamReader(fs);
                string info;
                if (!sReader.EndOfStream)
                    info = sReader.ReadLine();
                else
                    throw new Exception("No info line");
                DateTime date;
                if (!sReader.EndOfStream)
                    date = Convert.ToDateTime(sReader.ReadLine());
                else
                    throw new Exception("No date line");
                Info = info;
                Date = date;
                // now read time and (x,y,z)
                while (!sReader.EndOfStream)
                {
                    float t = ReadSingle(sReader);
                    float x = ReadSingle(sReader);
                    float y = ReadSingle(sReader);
                    float z = ReadSingle(sReader);
                    Vector3 grid = new Vector3(x, y, z);
                    Values.Add(new DataItem(t, grid));
                }
                sReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
        }

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

        public override string ToLongString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string newStr = "\n";
            for (int i = 0; i < Values.Count; i++) 
                newStr += Values[i].T + "\t" + Values[i].Value + '\n';

            string result_str = ToString() + newStr + "\n";
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return result_str;
        }

        public override IEnumerator<DataItem> GetEnumerator()
        {
            return ((IEnumerable<DataItem>)Values).GetEnumerator();
        }
    }
}