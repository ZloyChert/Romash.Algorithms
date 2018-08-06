using System;
using SearchTrees.Models;
using SearchTrees.Models.Interfaces;
using SearchTrees.Trees.Interfaces;

namespace SearchTrees.Trees.Abstract
{
    public abstract class BinaryTreeBase<TNode, TKey, TValue> : ISearchTree<TNode, TKey, TValue> 
        where TKey : IComparable<TKey>
        where TNode : NodeBase<TNode, TKey, TValue>, new()
    {
        #region Fields and Properties

        protected TNode RootNode { get; set; }

        #endregion

        #region Utils

        protected void NodeAgrumentNullCheck(TNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
        }

        #endregion

        #region Searching

        private TNode SearchNode(TNode node, TKey key)
        {
            while (node != null && node.Key.CompareTo(key) != 0)
            {
                node = key.CompareTo(node.Key) < 0 ? RootNode.LeftChildNode : RootNode.RightChildNode;
            }

            return node;
        }

        public TNode Search(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return SearchNode(RootNode, key);
        }

        #endregion

        #region Traversals

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

        #endregion

        #region Minimums and Maximums

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

        #endregion

        #region Iterating forward and back

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

        #endregion

        #region Inserting

        protected TNode BaseInsert(TNode newNode)
        {
            TNode tempNode = null;
            TNode rootCopy = RootNode;

            while (rootCopy != null)
            {
                tempNode = rootCopy;
                rootCopy = newNode.Key.CompareTo(rootCopy.Key) < 0 ? rootCopy.LeftChildNode : rootCopy.RightChildNode;
            }
            newNode.ParentNode = tempNode;

            if (tempNode == null)
            {
                RootNode = newNode;
            }
            else
            {
                if (newNode.Key.CompareTo(tempNode.Key) < 0)
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
        public abstract TNode Insert(TKey key, TValue value);

        #endregion

        #region Deleting

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

            var tempNode = exscindNode.LeftChildNode ?? exscindNode.RightChildNode;

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

            return exscindNode;
        }

        public void Delete(TKey key)
        {
            Delete(Search(key));
        }

        public abstract void Delete(TNode node);

        #endregion

    }
}
