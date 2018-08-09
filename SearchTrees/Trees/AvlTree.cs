using System;
using System.Collections.Generic;
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

        #region Constructors

        public AvlTree(IComparer<TKey> comparer) : base(comparer)
        {
        }

        public AvlTree()
        {
        }

        #endregion

        public Node<TKey, TValue> InsertIterative(TKey key, TValue value)
        {
            KeyAgrumentNullCheck(key);
            _nodesCount++;
            var tmp = BaseInsert(new Node<TKey, TValue>
            {
                Key = key,
                Value = value
            });
            return BalanceSubtree(tmp);
        }

        public override Node<TKey, TValue> Insert(TKey key, TValue value)
        {
            //return _nodesCount > _thresholdValue ? InsertIterative(key, value) : InsertRecursive(key, value);
            return InsertIterative(key, value);
        }

        public Node<TKey, TValue> InsertRecursive(TKey key, TValue value)
        {
            KeyAgrumentNullCheck(key);
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

        public override void Delete(Node<TKey, TValue> node)
        {
            var tmp = BaseDelete(node);
            BalanceSubtree(tmp);
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

        private Node<TKey, TValue> SingleRotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> tempNode = node.RightChildNode;
            tempNode.ParentNode = node.ParentNode;
            node.RightChildNode = tempNode.LeftChildNode;
            if (node.RightChildNode != null)
            {
                node.RightChildNode.ParentNode = node;
            }
            tempNode.LeftChildNode = node;
            node.ParentNode = tempNode;
            return tempNode;
        }

        private Node<TKey, TValue> SingleRotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> tempNode = node.LeftChildNode;
            tempNode.ParentNode = node.ParentNode;
            node.LeftChildNode = tempNode.RightChildNode;
            if (node.LeftChildNode != null)
            {
                node.LeftChildNode.ParentNode = node;
            }
            tempNode.RightChildNode = node;
            node.ParentNode = tempNode;
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
            Node<TKey, TValue> balancedPivot = null;
            while (tempNode != null)
            {
                Node<TKey, TValue> parentNode = tempNode.ParentNode;
                bool isLeft = tempNode.ParentNode?.LeftChildNode == tempNode;
                balancedPivot = Balance(tempNode);
                if (isLeft && parentNode != null)
                {
                    parentNode.LeftChildNode = balancedPivot;
                }
                if (!isLeft && parentNode != null)
                {
                    parentNode.RightChildNode = balancedPivot;
                }
                tempNode = balancedPivot.ParentNode;
            }
            RootNode = balancedPivot;
            return node;
        }
    }
}
