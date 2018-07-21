using System;
using SearchTrees.Models;
using SearchTrees.Trees.Interfaces;

namespace SearchTrees.Trees.Abstract
{
    public abstract class BinaryTreeBase<TKey, TValue> : ISearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        #region Fields and Properties

        protected Node<TKey, TValue> RootNode;

        #endregion

        #region Utils

        protected void NodeAgrumentNullCheck(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
        }

        #endregion

        #region Searching

        private Node<TKey, TValue> SearchNode(Node<TKey, TValue> node, TKey key)
        {
            while (node != null && node.Key.CompareTo(key) != 0)
            {
                node = key.CompareTo(node.Key) < 0 ? RootNode.LeftChildNode : RootNode.RightChildNode;
            }

            return node;
        }

        public Node<TKey, TValue> Search(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return SearchNode(RootNode, key);
        }

        #endregion

        #region Traversals

        private void LeftNodeTraversal(Node<TKey, TValue> node, Action<Node<TKey, TValue>> actionWithNode)
        {
            if (node != null)
            {
                LeftNodeTraversal(node.LeftChildNode, actionWithNode);
                actionWithNode(node);
                LeftNodeTraversal(node.RightChildNode, actionWithNode);
            }
        }
        private void RightNodeTraversal(Node<TKey, TValue> node, Action<Node<TKey, TValue>> actionWithNode)
        {
            if (node != null)
            {
                RightNodeTraversal(node.LeftChildNode, actionWithNode);
                actionWithNode(node);
                RightNodeTraversal(node.RightChildNode, actionWithNode);
            }
        }

        public void LeftTraversal(Action<Node<TKey, TValue>> actionWithNode)
        {
            if (actionWithNode == null)
            {
                throw new ArgumentNullException(nameof(actionWithNode));
            }

            LeftNodeTraversal(RootNode, actionWithNode);
        }

        public void RightTraversal(Action<Node<TKey, TValue>> actionWithNode)
        {
            if (actionWithNode == null)
            {
                throw new ArgumentNullException(nameof(actionWithNode));
            }

            RightNodeTraversal(RootNode, actionWithNode);
        }

        #endregion

        #region Minimums and Maximums

        public Node<TKey, TValue> MinimumNode(Node<TKey, TValue> node)
        {
            NodeAgrumentNullCheck(node);
            while (node.LeftChildNode != null)
            {
                node = node.LeftChildNode;
            }

            return node;
        }

        public Node<TKey, TValue> Minimum => MinimumNode(RootNode);

        public Node<TKey, TValue> MaximumNode(Node<TKey, TValue> node)
        {
            NodeAgrumentNullCheck(node);
            while (node.RightChildNode != null)
            {
                node = node.RightChildNode;
            }

            return node;
        }

        public Node<TKey, TValue> Maximum => MaximumNode(RootNode);

        #endregion

        #region Iterating forward and back

        public Node<TKey, TValue> SuccessorNode(Node<TKey, TValue> node)
        {
            NodeAgrumentNullCheck(node);

            if (node.RightChildNode != null)
            {
                return MinimumNode(node.RightChildNode);
            }
            Node<TKey, TValue> tempNode = node.ParentNode;

            while (tempNode != null && node == tempNode.RightChildNode)
            {
                node = tempNode;
                tempNode = node.ParentNode;
            }
            return tempNode;
        }

        public Node<TKey, TValue> PredecessorNode(Node<TKey, TValue> node)
        {
            NodeAgrumentNullCheck(node);

            if (node.LeftChildNode != null)
            {
                return MaximumNode(node.RightChildNode);
            }
            Node<TKey, TValue> tempNode = node.ParentNode;

            while (tempNode != null && node == tempNode.LeftChildNode)
            {
                node = tempNode;
                tempNode = node.ParentNode;
            }
            return tempNode;
        }

        #endregion

        #region Inserting

        protected Node<TKey, TValue> BaseInsert(TKey key, TValue value)
        {
            Node<TKey, TValue> tempNode = null;
            Node<TKey, TValue> rootCopy = RootNode;
            Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value);

            while (rootCopy != null)
            {
                tempNode = rootCopy;
                rootCopy = key.CompareTo(rootCopy.Key) < 0 ? rootCopy.LeftChildNode : rootCopy.RightChildNode;
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
        public abstract Node<TKey, TValue> Insert(TKey key, TValue value);

        #endregion

        #region Deleting

        protected Node<TKey, TValue> BaseDelete(Node<TKey, TValue> node)
        {
            NodeAgrumentNullCheck(node);
            Node<TKey, TValue> exscindNode;

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

        public abstract void Delete(Node<TKey, TValue> node);

        #endregion

    }
}
