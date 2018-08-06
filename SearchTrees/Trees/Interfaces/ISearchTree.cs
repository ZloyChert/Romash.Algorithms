using System;
using SearchTrees.Models;

namespace SearchTrees.Trees.Interfaces
{
    public interface ISearchTree<TNode, TKey, TValue> 
        where TKey : IComparable<TKey>
        where TNode : NodeBase<TNode, TKey, TValue>, new()
    {
        TNode Insert(TKey key, TValue value);
        void Delete(TNode node);
        TNode Search(TKey key);
    }
}
