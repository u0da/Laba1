using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Laba1
{
    class V1MainCollection : IEnumerable<V1Data>//, IEnumerable
    {
        private readonly List<V1Data> DataFields = new List<V1Data>();
        public int Count
        {
            get
            {
                if (DataFields == null)
                    return 0;
                else
                    return DataFields.Count;
            }
        }

        public int MaxNumberOfMesRes
        {
            get
            {
                int selector(V1Data selection)
                {
                    var query = from dataitem in selection
                                select dataitem;
                    return query.Count();
                }
                return DataFields.Max(selector);
            }
        }

        public IEnumerable<DataItem> EnumerationLength
        {
            get
            {
                var query = from v1dataOb in DataFields
                            from dataitem in v1dataOb
                            orderby dataitem.Value.Length() descending
                            select dataitem;
                return query;
            }
        }

        public IEnumerable<float> EnumerationTime
        {
            get
            {
                var TimeQuery = from v1dataOb in DataFields            //отбираем время
                             from dataitem in v1dataOb
                             group dataitem by dataitem.T;
                var query = from time in TimeQuery                     //отбираем только 1 раз
                            where time.Count() == 1
                            select time.Key;
                return query;
            }
        }

        public void Add(V1Data item)
        {
            DataFields.Add(item);
        }

        bool Remove(string id, DateTime dateTime) //?
        {
          bool result = false;
          for (int i = DataFields.Count - 1; i >= 0; i--)
          {
              if (DataFields[i].Info.Equals(id) && DataFields[i].Date.Equals(dateTime))
              {
                  DataFields.RemoveAt(i);
                  result = true;
              }
          }
            return result;
        }

        public void AddDefaults()
        {
            for (int i = 0; i < 2; i++) // length of vectors
            {
                V1DataOnGrid data = new V1DataOnGrid($"id={i}", DateTime.Now, new Grid(0, 0.5f, 10));
                data.InitRandom(-1f, 1f);
                Add(data);
            }
            for (int i = 1; i <= 2; i++) // amount of vectors
            {
                V1DataCollection data = new V1DataCollection($"id={i}", DateTime.Now);
                data.InitRandom(10 + 2 * i, 0f, 100f, -1f, 1f);
                Add(data);
            }

            Add(new V1DataOnGrid($"grid", DateTime.Now, new Grid(1f, 0, 0)));
            Add(new V1DataCollection($"collection", DateTime.Now)); 
        }

        public override string ToString()
        {
            string result = string.Join<V1Data>("\n", DataFields) + "\n";
            return $"{GetType().Name} \n {result}";
        }

        public IEnumerator GetEnumerator() //ОТЕЦ  public interface IEnumerable<out T> : IEnumerable
        {
            return ((IEnumerable)DataFields).GetEnumerator();
        }

        IEnumerator<V1Data> IEnumerable<V1Data>.GetEnumerator() // СЫН  public interface IEnumerable
        {                                                       //IEnumerator GetEnumerator();

            return ((IEnumerable<V1Data>)DataFields).GetEnumerator();
        }

        public string ToLongString(string format)
        {
            string result = "";
            for (int i = 0; i < Count; i++)
            {
                result += DataFields[i].ToLongString(format);
            }
            return result;
        }

    }

}
