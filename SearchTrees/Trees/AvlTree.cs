using System;
using SearchTrees.Extensions;
using SearchTrees.Models;
using SearchTrees.Trees.Abstract;

namespace SearchTrees.Trees
{
    public sealed class AvlTree<TKey, TValue> : BinaryTreeBase<Node<TKey, TValue>, TKey, TValue> 
        where TKey : IComparable<TKey>
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
            int nodeBfacror = node.GetBfactor<Node<TKey, TValue>, TKey, TValue>();
            if (nodeBfacror == 2)
            {
                if (node.RightChildNode.GetBfactor<Node<TKey, TValue>, TKey, TValue>() < 0)
                {
                    node.RightChildNode = SingleRotateRight(node.RightChildNode);
                }
                return SingleRotateLeft(node);
            }
            if (nodeBfacror == -2)
            {
                if (node.LeftChildNode.GetBfactor<Node<TKey, TValue>, TKey, TValue>() > 0)
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
            _nodesCount++;
            return BalanceSubtree(BaseInsert(new Node<TKey, TValue>
            {
                Key = key,
                Value = value
            }));
        }

        public override Node<TKey, TValue> Insert(TKey key, TValue value)
        {
            return _nodesCount > _thresholdValue ? InsertIterative(key, value) : InsertRecursive(key, value);
        }

        public Node<TKey, TValue> InsertRecursive(TKey key, TValue value)
        {
            _nodesCount++;
            if (RootNode == null)
            {
                RootNode = new Node<TKey, TValue>
                {
                    Key = key,
                    Value = value
                };
                return RootNode;
            }

            return RootNode = InsertRecursiveNode(RootNode, key, value);
        }

        private Node<TKey, TValue> InsertRecursiveNode(Node<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null)
            {
                return new Node<TKey, TValue>
                {
                    Key = key,
                    Value = value
                };
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
