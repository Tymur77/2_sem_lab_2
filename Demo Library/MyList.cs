using System;
using System.Linq;
using System.Collections;

namespace DemoLibrary
{
    public class MyList<T> : IEnumerable
    {
        private T[] values;

        public int Length { get; private set; }

        public void Append(T value)
        {
            if (values == null)
            {
                values = new T[] { value };
                Length = 1;
                return;
            }
            values = values.Append(value).ToArray();
            Length++;
        }

        public T this[int index]
        {
            get => values[index];
        }

        public T Pop(int index)
        {
            T value = values[index];
            values = values.Take(index)
                .Concat(values.Skip(index + 1))
                .ToArray();
            Length--;
            return value;
        }

        public IEnumerator GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public T[] GetCopy()
        {
            return values.ToArray();
        }

    }
}
