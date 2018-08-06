using System;
using SearchTrees.Models;

namespace SearchTrees.Extensions
{
    internal static class NodeExtensions
    {
        internal static int GetHeight<TKey, TValue>(this Node<TKey, TValue> node) 
            where TKey: IComparable<TKey>
        {
            if (node == null)
            {
                return -1;
            }

            int leftHeight = node.LeftChildNode.GetHeight();
            int rightHeight = node.RightChildNode.GetHeight();
            return leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
        }

        internal static int GetBfactor<TNode, TKey, TValue>(this TNode node) 
            where TKey : IComparable<TKey>
            where TNode : Node<TKey, TValue> , new()
        {
            return node.RightChildNode.GetHeight() - node.LeftChildNode.GetHeight();
        }
    }
}
