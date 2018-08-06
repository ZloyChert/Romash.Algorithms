using System;
using SearchTrees.Models;
using SearchTrees.Trees.Abstract;

namespace SearchTrees.Trees
{
    public sealed class RedBlackTree<TKey, TValue> : BinaryTreeBase<ColorNode<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        #region Balancing

        private void RotateLeft(ColorNode<TKey, TValue> node)
        {
            ColorNode<TKey, TValue> tempNode = node.RightChildNode;
            node.RightChildNode = tempNode.LeftChildNode;

            if (tempNode.LeftChildNode != null)
            {
                tempNode.LeftChildNode.ParentNode = node;
            }
            tempNode.ParentNode = node.ParentNode;

            if (node.ParentNode == null)
            {
                RootNode = tempNode;
            }
            else
            {
                if (node == node.ParentNode.LeftChildNode)
                {
                    node.ParentNode.LeftChildNode = tempNode;
                }
                else
                {
                    node.ParentNode.RightChildNode = tempNode;
                }
            }
            tempNode.LeftChildNode = node;
            node.ParentNode = tempNode;
        }

        private void RotateRight(ColorNode<TKey, TValue> node)
        {
            ColorNode<TKey, TValue> tempNode = node.LeftChildNode;
            node.LeftChildNode = tempNode.RightChildNode;

            if (tempNode.RightChildNode != null)
            {
                tempNode.RightChildNode.ParentNode = node;
            }
            tempNode.ParentNode = node.ParentNode;

            if (node.ParentNode == null)
            {
                RootNode = tempNode;
            }
            else
            {
                if (node == node.ParentNode.RightChildNode)
                {
                    node.ParentNode.RightChildNode = tempNode;
                }
                else
                {
                    node.ParentNode.LeftChildNode = tempNode;
                }
            }
            tempNode.RightChildNode = node;
            node.ParentNode = tempNode;
        }

        private ColorNode<TKey, TValue> BalanceAfterInsert(ColorNode<TKey, TValue> newNode)
        {
            while (newNode != RootNode && newNode.ParentNode.Red)
            {
                ColorNode<TKey, TValue> tempNode;
                if (newNode.ParentNode == newNode.ParentNode.ParentNode.LeftChildNode)
                {
                    tempNode = newNode.ParentNode.ParentNode.RightChildNode;
                    if (tempNode != null && tempNode.Red)
                    {
                        newNode.ParentNode.Black = true;
                        tempNode.Black = true;
                        newNode.ParentNode.ParentNode.Red = true;
                        newNode = newNode.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (newNode == newNode.ParentNode.RightChildNode)
                        {
                            newNode = newNode.ParentNode;
                            if (newNode.RightChildNode != null)
                            {
                                RotateLeft(newNode);
                            }
                        }
                        newNode.ParentNode.Black = true;
                        newNode.ParentNode.ParentNode.Red = true;
                        if (newNode.ParentNode.ParentNode.LeftChildNode != null)
                        {
                            RotateRight(newNode.ParentNode.ParentNode);
                        }
                    }
                }
                else
                {
                    tempNode = newNode.ParentNode.ParentNode.LeftChildNode;
                    if (tempNode != null && tempNode.Red)
                    {
                        newNode.ParentNode.Black = true;
                        tempNode.Black = true;
                        newNode.ParentNode.ParentNode.Red = true;
                        newNode = newNode.ParentNode.ParentNode;
                    }
                    else
                    {
                        if (newNode == newNode.ParentNode.LeftChildNode)
                        {
                            newNode = newNode.ParentNode;
                            if (newNode.LeftChildNode != null)
                            {
                                RotateRight(newNode);
                            }
                        }
                        newNode.ParentNode.Black = true;
                        newNode.ParentNode.ParentNode.Red = true;
                        if (newNode.ParentNode.ParentNode.RightChildNode != null)
                        {
                            RotateLeft(newNode.ParentNode.ParentNode);
                        }
                    }
                }
            }
            RootNode.Black = true;

            return newNode;
        }
        private ColorNode<TKey, TValue> BalanceAfterDelete(ColorNode<TKey, TValue> newNode)
        {
            while (newNode != RootNode && newNode.Black)
            {
                if (newNode == newNode.ParentNode.LeftChildNode)
                {
                    var tempNode = newNode.ParentNode.RightChildNode;
                    if (tempNode.Red)
                    {
                        tempNode.Black = true;
                        tempNode.ParentNode.Red = true;
                        RotateLeft(newNode.ParentNode);
                        tempNode = newNode.ParentNode.RightChildNode;
                    }
                    if (tempNode.LeftChildNode.Black && tempNode.RightChildNode.Black)
                    {
                        tempNode.Red = true;
                        newNode = newNode.ParentNode;
                    }
                    else
                    {
                        if (tempNode.RightChildNode.Black)
                        {
                            tempNode.LeftChildNode.Black = true;
                            tempNode.Red = true;
                            RotateRight(tempNode);
                            tempNode = newNode.ParentNode.RightChildNode;
                        }
                        tempNode.Red = newNode.ParentNode.Red;
                        newNode.ParentNode.Black = true;
                        tempNode.ParentNode.Black = true;
                        RotateLeft(newNode.ParentNode);
                        newNode = RootNode;
                    }
                }
                else
                {
                    var tempNode = newNode.ParentNode.LeftChildNode;
                    if (tempNode.Red)
                    {
                        tempNode.Black = true;
                        tempNode.ParentNode.Red = true;
                        RotateLeft(newNode.ParentNode);
                        tempNode = newNode.ParentNode.LeftChildNode;
                    }
                    if (tempNode.LeftChildNode.Black && tempNode.RightChildNode.Black)
                    {
                        tempNode.Red = true;
                        newNode = newNode.ParentNode;
                    }
                    else
                    {
                        if (tempNode.LeftChildNode.Black)
                        {
                            tempNode.RightChildNode.Black = true;
                            tempNode.Red = true;
                            RotateRight(tempNode);
                            tempNode = newNode.ParentNode.LeftChildNode;
                        }
                        tempNode.Red = newNode.ParentNode.Red;
                        newNode.ParentNode.Black = true;
                        tempNode.ParentNode.Black = true;
                        RotateLeft(newNode.ParentNode);
                        newNode = RootNode;
                    }
                }
            }

            return newNode;
        }
        #endregion

        public override ColorNode<TKey, TValue> Insert(TKey key, TValue value)
        {
            var newNode = new ColorNode<TKey, TValue>
            {
                Key = key,
                Value = value,
                Red = true
            };
            BaseInsert(newNode);
            return BalanceAfterInsert(newNode);
        }

        public override void Delete(ColorNode<TKey, TValue> node)
        {
            var deleted = BaseDelete(node);
            BalanceAfterDelete(node);
        }
    }
}
