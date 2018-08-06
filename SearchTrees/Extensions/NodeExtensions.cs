using System;
using SearchTrees.Models;

namespace SearchTrees.Extensions
{
    public static class NodeExtensions
    {
        public static int GetHeight<TNode, TKey, TValue>(this NodeBase<TNode, TKey, TValue> node) 
            where TNode : NodeBase<TNode, TKey, TValue> , new()
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
