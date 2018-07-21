using System;
using SearchTrees.Models.Interfaces;

namespace SearchTrees.Models
{
    public class Node<TKey, TValue> : IBinaryTreeNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        public Node(TKey key, TValue value, Node<TKey, TValue> parentNode = null, Node<TKey, TValue> leftChildNode = null, Node<TKey, TValue> rightChildNode = null)
        {
            Key = key;
            Value = value;
            ParentNode = parentNode;
            LeftChildNode = leftChildNode;
            RightChildNode = rightChildNode;
        }

        public Node<TKey, TValue> ParentNode { get; internal set; }
        public Node<TKey, TValue> LeftChildNode { get; internal set; }
        public Node<TKey, TValue> RightChildNode { get; internal set; }
        public TKey Key { get; internal set; }
        public TValue Value { get; set; }
    }
}
