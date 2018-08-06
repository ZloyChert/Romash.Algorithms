using System;

namespace SearchTrees.Models
{
    public class Node<TKey, TValue> : NodeBase<Node<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }
}
