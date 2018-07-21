using System;
using SearchTrees.Models;
using SearchTrees.Models.Interfaces;

namespace SearchTrees.Trees.Interfaces
{
    public interface ISearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        Node<TKey, TValue> InsertIterative(TKey key, TValue value);
        void Delete(Node<TKey, TValue> node);
        Node<TKey, TValue> Search(TKey key);
    }
}
