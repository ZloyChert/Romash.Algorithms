using System;
using System.Collections.Generic;
using SearchTrees.Exceptions;
using SearchTrees.Models;
using SearchTrees.Trees.Interfaces;

namespace SearchTrees.Trees.Abstract
{
    public abstract class BinaryTreeBase<TNode, TKey, TValue> : ISearchTree<TNode, TKey, TValue> 
        where TNode : NodeBase<TNode, TKey, TValue>, new()
    {
        #region Fields and Properties

        protected IComparer<TKey> Comparer;
        protected TNode RootNode { get; set; }

        #endregion

        #region Constructors

        protected BinaryTreeBase(IComparer<TKey> comparer)
        {
            Comparer = comparer;
        }

        protected BinaryTreeBase()
        {
            Comparer = Comparer<TKey>.Default;
        }

        #endregion

        public abstract TNode Insert(TKey key, TValue value);

        public abstract void Delete(TNode node);

        public void Delete(TKey key)
        {
            KeyAgrumentNullCheck(key);
            TNode nodeForDeleting = Search(key);
            if (nodeForDeleting == null)
            {
                return;
            }
            Delete(nodeForDeleting);
        }

        public TNode Search(TKey key)
        {
            KeyAgrumentNullCheck(key);
            return SearchNode(RootNode, key);
        }

        public void LeftTraversal(Action<TNode> actionWithNode)
        {
            if (actionWithNode == null)
            {
                throw new ArgumentNullException(nameof(actionWithNode));
            }

            LeftNodeTraversal(RootNode, actionWithNode);
        }

        public void RightTraversal(Action<TNode> actionWithNode)
        {
            if (actionWithNode == null)
            {
                throw new ArgumentNullException(nameof(actionWithNode));
            }

            RightNodeTraversal(RootNode, actionWithNode);
        }

        public TNode MinimumNode(TNode node)
        {
            NodeAgrumentNullCheck(node);
            while (node.LeftChildNode != null)
            {
                node = node.LeftChildNode;
            }

            return node;
        }

        public TNode Minimum => MinimumNode(RootNode);

        public TNode MaximumNode(TNode node)
        {
            NodeAgrumentNullCheck(node);
            while (node.RightChildNode != null)
            {
                node = node.RightChildNode;
            }

            return node;
        }

        public TNode Maximum => MaximumNode(RootNode);

        public TNode SuccessorNode(TNode node)
        {
            NodeAgrumentNullCheck(node);

            if (node.RightChildNode != null)
            {
                return MinimumNode(node.RightChildNode);
            }
            TNode tempNode = node.ParentNode;

            while (tempNode != null && node == tempNode.RightChildNode)
            {
                node = tempNode;
                tempNode = node.ParentNode;
            }
            return tempNode;
        }

        public TNode PredecessorNode(TNode node)
        {
            NodeAgrumentNullCheck(node);

            if (node.LeftChildNode != null)
            {
                return MaximumNode(node.RightChildNode);
            }
            TNode tempNode = node.ParentNode;

            while (tempNode != null && node == tempNode.LeftChildNode)
            {
                node = tempNode;
                tempNode = node.ParentNode;
            }
            return tempNode;
        }

        protected TNode BaseInsert(TNode newNode)
        {
            TNode tempNode = null;
            TNode rootCopy = RootNode;

            while (rootCopy != null)
            {
                tempNode = rootCopy;
                int keyComparisionResult = Comparer.Compare(newNode.Key, rootCopy.Key);
                if (keyComparisionResult == 0)
                {
                    throw new SearchTreeArgumentException($"Argument {nameof(newNode)} key already exists");
                }

                rootCopy = keyComparisionResult < 0 ? rootCopy.LeftChildNode : rootCopy.RightChildNode;
            }
            newNode.ParentNode = tempNode;

            if (tempNode == null)
            {
                RootNode = newNode;
            }
            else
            {
                if (Comparer.Compare(newNode.Key, tempNode.Key) < 0)
                {
                    tempNode.LeftChildNode = newNode;
                }
                else
                {
                    tempNode.RightChildNode = newNode;
                }
            }
            return newNode;
        }

        protected TNode BaseDelete(TNode node)
        {
            NodeAgrumentNullCheck(node);
            TNode exscindNode;

            if (node.LeftChildNode == null || node.RightChildNode == null)
            {
                exscindNode = node;
            }
            else
            {
                exscindNode = SuccessorNode(node);
            }
            TNode parentNode = exscindNode.ParentNode;

            TNode tempNode = exscindNode.LeftChildNode ?? exscindNode.RightChildNode;

            if (tempNode != null)
            {
                tempNode.ParentNode = exscindNode.ParentNode;
            }
            if (exscindNode.ParentNode == null)
            {
                RootNode = tempNode;
            }
            else
            {
                if (exscindNode == exscindNode.ParentNode.LeftChildNode)
                {
                    exscindNode.ParentNode.LeftChildNode = tempNode;
                }
                else
                {
                    exscindNode.ParentNode.RightChildNode = tempNode;
                }
            }

            if (exscindNode != node)
            {
                node.Key = exscindNode.Key;
                node.Value = exscindNode.Value;
            }

            return tempNode ?? parentNode;
        }

        protected void NodeAgrumentNullCheck(TNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
        }

        protected void KeyAgrumentNullCheck(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
        }

        private TNode SearchNode(TNode node, TKey key)
        {
            while (node != null && Comparer.Compare(node.Key, key) != 0)
            {
                node = Comparer.Compare(key, node.Key) < 0 ? node.LeftChildNode : node.RightChildNode;
            }

            return node;
        }

        private void LeftNodeTraversal(TNode node, Action<TNode> actionWithNode)
        {
            if (node != null)
            {
                LeftNodeTraversal(node.LeftChildNode, actionWithNode);
                actionWithNode(node);
                LeftNodeTraversal(node.RightChildNode, actionWithNode);
            }
        }

        private void RightNodeTraversal(TNode node, Action<TNode> actionWithNode)
        {
            if (node != null)
            {
                RightNodeTraversal(node.LeftChildNode, actionWithNode);
                actionWithNode(node);
                RightNodeTraversal(node.RightChildNode, actionWithNode);
            }
        }
    }
}
