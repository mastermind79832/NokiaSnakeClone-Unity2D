using System;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame
{
    public class GenericPool<T>
    {
        private Queue<T> m_Pool = new Queue<T>();
        public void AddToPool(T item) => m_Pool.Enqueue(item);   
        public T GetItem() => m_Pool.Dequeue();
        public bool IsEmpty() => m_Pool.Count == 0;
    }
}
