using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NS
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private List<T> list = new();


        //要素をリストの最後尾に追加
        public void Enqueue(T item)
        {
            list.Add(item);
        }

        //リストの先頭の要素を取り出して削除
        public T Dequeue(int index = 0)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("LinkedList:要素が存在しません");
            }

            if (index >= list.Count)
            {
                throw new IndexOutOfRangeException("指定したindexは要素数を超えています");
            }

            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public int FindIndex(Predicate<T> predicate)
        {
            return list.FindIndex(predicate);
        }

        // キューの先頭要素を取得
        public T Peek()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("LinkedList:要素が存在しません");
            }

            return list[0];
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public int Count()
        {
            return list.Count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


