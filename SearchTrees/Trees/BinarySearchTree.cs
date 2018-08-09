using System;
using System.Collections.Generic;
using SearchTrees.Models;
using SearchTrees.Trees.Abstract;

namespace SearchTrees.Trees
{
    public class BinarySearchTree<TKey, TValue> : BinaryTreeBase<Node<TKey, TValue>, TKey, TValue> 
        where TKey : IComparable<TKey>
    {
        #region Constructors

        public BinarySearchTree(IComparer<TKey> comparer) : base(comparer)
        {
        }

        public BinarySearchTree()
        {
        }

        #endregion

        public override Node<TKey, TValue> Insert(TKey key, TValue value)
        {
            return BaseInsert(new Node<TKey, TValue>
            {
                Key = key,
                Value = value
            });
        }

        public override void Delete(Node<TKey, TValue> node)
        {
            BaseDelete(node);
        }
    }
}
