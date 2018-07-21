using System;

namespace SearchTrees.Models.Interfaces
{
    public interface IBinaryTreeNode<TKey, TValue> : INode<TKey, TValue> where TKey : IComparable<TKey>
    {
        Node<TKey, TValue> ParentNode { get; }
        Node<TKey, TValue> LeftChildNode { get; }
        Node<TKey, TValue> RightChildNode { get; }
    }
}
