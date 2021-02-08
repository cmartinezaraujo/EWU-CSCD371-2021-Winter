using System;

namespace GenericsHomework
{
    public class Node<T>
    {
        private T? _Value;
        private Node<T>? _Next;

        public T Value { get => _Value!; private  set => _Value = value;}

        public Node<T> Next
        {
            get => _Next!;
            set
            {
                value._Next = this;
                _Next = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Node(T value)
        {
            Value = value;
            Next = this;
        }

        public override string ToString()
        {
            if(Value is null)
            {
                throw new ArgumentNullException(nameof(Value));
            }

            return Value.ToString()!;
        }

        public void Insert(T value)
        {
            Node<T> newNode = new Node<T>(value);
            this.Next = newNode;
        }
    }
}
