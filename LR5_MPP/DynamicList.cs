using System;
using System.Collections;
using System.Collections.Generic;

namespace LR5_MPP
{
    [ExportClass]
    public class DynamicList<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 4;
        private const int MaxArrayLength = 0X7FEFFFFF;
        private const string IndexOutOfRangeEx = "Index is out of range.";
        private const string CapacityLessValueEx = "New value of capacity less than number of elements.";
        private const string CapacityLessZeroEx = "Capacity cannot be less than zero.";
        private const string EnumOperCantHappenEx = "Enumeration operation cannot happen.";

        private T[] array;
        private int count;
        static readonly T[] emptyArray = Array.Empty<T>();

        public int Capacity
        {
            get { return array.Length; }
            set
            {
                if (value < count)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), CapacityLessValueEx);
                }

                if (value != array.Length)
                {
                    if (value > 0)
                    {
                        T[] newArray = new T[value];
                        if (count > 0)
                        {
                            Array.Copy(array, newArray, count);
                        }

                        array = newArray;
                    }
                    else
                    {
                        array = emptyArray;
                    }
                }
            }
        }
        public int Count { get => count; }
        public T[] Items { get => array; }

        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), IndexOutOfRangeEx);
                }

                return array[index];
            }
            set
            {
                if ((uint)index >= (uint)count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), IndexOutOfRangeEx);
                }

                array[index] = value;
            }
        }

        public DynamicList()
        {
            array = emptyArray;
        }

        public DynamicList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), CapacityLessZeroEx);
            }

            if (capacity == 0)
            {
                array = emptyArray;
            }
            else
            {
                array = new T[capacity];
            }
        }

        public void Add(T newElement)
        {
            if (count == array.Length)
            {
                EnsureCapacity(count + 1);
            }

            array[count++] = newElement;
        }

        public void Clear()
        {
            if (count > 0)
            {
                Array.Clear(array, 0, count);
                count = 0;
            }
        }

        public bool Remove(T value)
        {
            int valueIndex = Array.IndexOf<T>(array, value);
            if (valueIndex >= 0)
            {
                RemoveAt(valueIndex);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), IndexOutOfRangeEx);
            }

            count--;
            if (index < count)
            {
                Array.Copy(array, index + 1, array, index, count - index);
            }

            array[count] = default;
        }

        private void EnsureCapacity(int min)
        {
            if (array.Length < min)
            {
                int newCapacity = array.Length == 0 ? DefaultCapacity : array.Length * 2;
                if ((uint)newCapacity > MaxArrayLength)
                    newCapacity = MaxArrayLength;

                if (newCapacity < min)
                    newCapacity = min;

                Capacity = newCapacity;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        class Enumerator : IEnumerator<T>
        {
            private readonly DynamicList<T> dynamicList;

            private int index;
            private T current;

            public Enumerator(DynamicList<T> dynamicList)
            {
                this.dynamicList = dynamicList;
                index = 0;
                current = default;
            }

            public T Current { get => current; }

            object IEnumerator.Current
            {
                get
                {
                    if (index == 0 || index == dynamicList.count + 1)
                    {
                        throw new InvalidOperationException(EnumOperCantHappenEx);
                    }

                    return Current;
                }
            }

            public void Dispose()
            {
                // Method intentionally left empty.
            }

            public bool MoveNext()
            {
                if ((uint)index < (uint)dynamicList.count)
                {
                    current = dynamicList.array[index];
                    index++;
                    return true;
                }

                index = dynamicList.count + 1;
                current = default;

                return false;
            }

            public void Reset()
            {
                index = 0;
                current = default;
            }
        }
    }
}
