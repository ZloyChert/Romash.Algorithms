using System;
using SearchTrees.Extensions;
using SearchTrees.Models;
using SearchTrees.Trees.Abstract;

namespace SearchTrees.Trees
{
    public class AvlTree<TKey, TValue> : BinaryTreeBase<TKey, TValue> where TKey : IComparable<TKey>
    {
        #region Fields and properties

        private int _nodesCount;
        private readonly int _thresholdValue = 60000;

        #endregion

        #region Balancing

        private Node<TKey, TValue> SingleRotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> tempNode = node.RightChildNode;
            node.RightChildNode = tempNode.LeftChildNode;
            tempNode.LeftChildNode = node;
            return tempNode;
        }
        private Node<TKey, TValue> SingleRotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> tempNode = node.LeftChildNode;
            node.LeftChildNode = tempNode.RightChildNode;
            tempNode.RightChildNode = node;
            return tempNode;
        }
        private Node<TKey, TValue> Balance(Node<TKey, TValue> node)
        {
            int nodeBfacror = node.GetBfactor();
            if (nodeBfacror == 2)
            {
                if (node.RightChildNode.GetBfactor() < 0)
                {
                    node.RightChildNode = SingleRotateRight(node.RightChildNode);
                }
                return SingleRotateLeft(node);
            }
            if (nodeBfacror == -2)
            {
                if (node.LeftChildNode.GetBfactor() > 0)
                {
                    node.LeftChildNode = SingleRotateLeft(node.LeftChildNode);
                }
                return SingleRotateRight(node);
            }
            return node;
        }

        private Node<TKey, TValue> BalanceSubtree(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> tempNode = node;
            while (tempNode != null)
            {
                Balance(tempNode);
                tempNode = tempNode.ParentNode;
            }
            return node;
        }

        #endregion

        #region Inserting

        public Node<TKey, TValue> InsertIterative(TKey key, TValue value)
        {
            return BalanceSubtree(BaseInsert(key, value));
        }

        public override Node<TKey, TValue> Insert(TKey key, TValue value)
        {
            _nodesCount++;
            return _nodesCount > _thresholdValue ? InsertIterative(key, value) : InsertRecursive(key, value);
        }

        public Node<TKey, TValue> InsertRecursive(TKey key, TValue value)
        {
            if (RootNode == null)
            {
                RootNode = new Node<TKey, TValue>(key, value);
                return RootNode;
            }

            return InsertRecursiveNode(RootNode, key, value);
        }

        private Node<TKey, TValue> InsertRecursiveNode(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null)
            {
                return new Node<TKey, TValue>(key, value);
            }

            if (key.CompareTo(node.Key) < 0)
            {
                node.LeftChildNode = InsertRecursiveNode(node.LeftChildNode, key, value);
            }
            else
            {
                node.RightChildNode = InsertRecursiveNode(node.RightChildNode, key, value);
            }
            return Balance(node);
        }

        #endregion

        #region Deleting

        public override void Delete(Node<TKey, TValue> node)
        {
            BalanceSubtree(BaseDelete(node));
        }

        #endregion
    }
}
