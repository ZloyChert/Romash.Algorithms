using System;
using SearchTrees.Models;

namespace SearchTrees.Extensions
{
    internal static class NodeExtensions
    {
        internal static int GetHeight<TKey, TValue>(this Node<TKey, TValue> node) where TKey: IComparable<TKey>
        {
            if (node == null)
            {
                return -1;
            }

            int leftHeight = node.LeftChildNode.GetHeight();
            int rightHeight = node.RightChildNode.GetHeight();
            return (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        internal static int GetBfactor<TKey, TValue>(this Node<TKey, TValue> node) where TKey : IComparable<TKey>
        {
            return node.RightChildNode.GetHeight() - node.LeftChildNode.GetHeight();
        }
    }
}
