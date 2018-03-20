namespace GFileFormatter
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An instance of the <see cref="ListBuffer{R}"/> class represents a dynamic array with public underlaying buffer.
    /// </summary>
    /// <typeparam name="R">Type of elements in the list.</typeparam>
    internal class ListBuffer<R>
    {
        /// <summary>
        /// The default capacity of a list buffer.
        /// </summary>
        private const int DEFAULT_CAPACITY = 4;

        /// <summary>
        /// The number of elements in a list.
        /// </summary>
        private int size;

        /// <summary>
        /// The underlaying buffer of a list.
        /// </summary>
        private R[] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBuffer{R}"/> class.
        /// </summary>
        public ListBuffer()
        {
            this.buffer = new R[DEFAULT_CAPACITY];
            this.size = 0;
        }

        /// <summary>
        /// Gets the underlaying buffer.
        /// </summary>
        public R[] Buffer => this.buffer;

        /// <summary>
        /// Gets the number of elements in a list.
        /// </summary>
        public int Size => this.size;

        /// <summary>
        /// Gets or sets maximum capacity of the underlaying buffer.
        /// </summary>
        public int Capacity
        {
            get
            {
                return this.buffer.Length;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                Array.Resize(ref this.buffer, value);
            }
        }

        /// <summary>
        /// Adds a new item in a list. If number of elements is bigger, then a capacity of a buffer is increased two times.
        /// </summary>
        /// <param name="item">The item that is going to be added to the list.</param>
        public void Add(R item)
        {
            if (this.size == this.buffer.Length)
            {
                Array.Resize(ref this.buffer, this.buffer.Length * 2);
            }

            this.buffer[this.size++] = item;
        }

        /// <summary>
        /// Adds new items from an array to the list. If possible insertion can exceed maximum capacity of a buffer,
        /// then it is increased by the number of elements in the given array <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The array of items that are going to be added to the list.</param>
        public void AddRange(R[] items)
        {
            if (this.size + items.Length > this.buffer.Length)
            {
                Array.Resize(ref this.buffer, this.size + items.Length);
            }

            Array.Copy(items, 0, this.buffer, this.size, items.Length);
            this.size += items.Length;
        }

        /// <summary>
        /// Adds new items from a list to a list buffer. If possible insertion can exceed maximum capacity of a buffer,
        /// then it is increased by the number of elements in the given list <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The list of items that are going to be added to the list buffer.</param>
        public void AddRange(List<R> items)
        {
            if (this.size + items.Count > this.buffer.Length)
            {
                Array.Resize(ref this.buffer, this.size + items.Count);
            }

            items.CopyTo(this.buffer, this.size);
            this.size += items.Count;
        }
    }
}
