using System;
using System.Collections;
using System.Collections.Generic;

namespace Laba1
{
    class V1MainCollection : IEnumerable<V1Data>//, IEnumerable
    {
        private List<V1Data> DataFields = new List<V1Data>();
        public int Count { get { return DataFields.Count; } }

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

    }

}
