using System;

namespace SearchTrees.Models.Interfaces
{
    public interface INode<TKey, TValue> where TKey : IComparable<TKey>
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}
