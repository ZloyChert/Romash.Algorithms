using System;
using SearchTrees.Models;
using SearchTrees.Trees.Abstract;

namespace SearchTrees.Trees
{
    public class BinarySearchTree<TKey, TValue> : BinaryTreeBase<TKey, TValue> where TKey : IComparable<TKey>
    {
        public override Node<TKey, TValue> Insert(TKey key, TValue value)
        {
            return BaseInsert(key, value);
        }

        public override void Delete(Node<TKey, TValue> node)
        {
            BaseDelete(node);
        }
    }
}
