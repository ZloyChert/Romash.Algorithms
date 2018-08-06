using System;
using SearchTrees.Models.Interfaces;

namespace SearchTrees.Models
{
    public class NodeBase<TNode, TKey, TValue> : INode<TKey, TValue> 
        where TKey : IComparable<TKey>
        where TNode : NodeBase<TNode, TKey, TValue>, new()
    {
        public TNode ParentNode { get; internal set; }
        public TNode LeftChildNode { get; internal set; }
        public TNode RightChildNode { get; internal set; }
        public TKey Key { get; internal set; }
        public TValue Value { get; set; }
    }
}
