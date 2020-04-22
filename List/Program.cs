using System;
using System.Collections;
using System.Collections.Generic;

namespace List
{
    public class Node<T>
    {
        public T Value { get; }
        public Node<T> Next { get; set; }

        public Node(T value) { Value = value; }
    }

    public class List<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;
        private int _count;

        public bool IsEmpty => _count == 0;
        public int Count => _count;

        public void Push_Back(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var node = new Node<T>(value);
            if (_head == null)
                _head = node;
            else
                _tail.Next = node;

            _tail = node;
            _count++;
        }
        
        public void Push_Forward(T value)
        {
            var node = new Node<T>(value);
            node.Next = _head;
            _head = node;
            if (IsEmpty)
                _tail = _head;

            _count++;
        }
        
        public bool Remove(T value)
        {
            Node<T> cur = _head;
            Node<T> prev = null;
            while (cur != null)
            {
                if (value != null && cur.Value.Equals(value))
                {
                    if (prev != null)
                    {
                        prev.Next = cur.Next;
                        if (cur.Next == null)
                            _tail = prev;
                    }
                    else
                    {
                        _head = _head.Next;
                        if (_head == null)
                            _tail = null;
                    }

                    _count--;
                    return true;
                }

                prev = cur;
                cur = cur.Next;
            }

            return false;
        }
        
        public void Reverse()
        {
            Node<T> cur = _head;
            Node<T> prev = null;
            Node<T> next;

            while (cur != null)
            {
                next = cur.Next;
                if (prev != null)
                {
                    cur.Next = prev;
                }
                else
                {
                    cur.Next = _tail.Next;
                    _tail = cur;
                }

                prev = cur;
                cur = next;
            }

            _head = prev;
        }
        
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }       

        public IEnumerator<T> GetEnumerator()
        {
            var cur = _head;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }

        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable).GetEnumerator();
        }
    }

    

    public static class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<int>();

            Console.WriteLine("Push Back:");

            list.Push_Back(1);
            list.Push_Back(2);
            list.Push_Back(3);

            foreach (var item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            Console.WriteLine("Remove:");

            list.Remove(2);

            foreach (var item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            Console.WriteLine("Push Forward:");

            list.Push_Forward(0);

            foreach (var item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            Console.WriteLine("Reverse:");

            list.Reverse();

            foreach (var item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            list.Clear();
        }
    }
}
