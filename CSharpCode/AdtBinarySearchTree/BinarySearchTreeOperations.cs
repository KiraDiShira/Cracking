using System;
using System.Collections.Generic;

namespace AdtBinarySearchTree
{
    public class BinarySearchTreeOperations
    {
        public TreeNode Find(int key, TreeNode root)
        {
            if (key == root.Key)
            {
                return root;
            }

            if (key < root.Key)
            {
                if (root.Left == null)
                {
                    return root;
                }

                return Find(key, root.Left);
            }

            if (key > root.Key)
            {
                if (root.Right == null)
                {
                    return root;
                }

                return Find(key, root.Right);
            }

            throw new Exception("Illegal Find execution");
        }

        public TreeNode Next(TreeNode node)
        {
            if (node.Right != null)
            {
                return LeftDescendant(node.Right);
            }

            return RightAncestor(node);
        }

        public IList<TreeNode> RangeSearch(int x, int y, TreeNode root)
        {
            IList<TreeNode> range = new List<TreeNode>();

            TreeNode node = Find(x, root);

            while (node.Key <= y)
            {
                if (node.Key >= x)
                {
                    range.Add(node);
                }

                node = Next(node);
            }

            return range;
        }

        public void Insert(int key, TreeNode root)
        {
            var parent = Find(key, root);

            var nodeToInsert = new TreeNode()
            {
                Key = key,
                Parent = parent
            };

            if (key < parent.Key)
            {
                parent.Left = nodeToInsert;
            }
            else
            {
                parent.Right = nodeToInsert;
            }
        }

        public void Delete(TreeNode nodeToDelete)
        {         
            if (nodeToDelete.Right == null)
            {
                if (nodeToDelete.Parent.Key > nodeToDelete.Key)
                {
                    nodeToDelete.Parent.Left = nodeToDelete.Left;
                }
                else
                {
                    nodeToDelete.Parent.Right = nodeToDelete.Left;
                }
            }
            else
            {
                TreeNode next = Next(nodeToDelete);
                nodeToDelete.Key = next.Key;
                next.Key = next.Right.Key;
                next.Left = next.Right.Left;
                next.Parent = next.Right.Parent;
                next.Right = next.Right.Right;
            }
        }

        public void RotateRight(TreeNode x)
        {
            TreeNode xParent = x.Parent;
            TreeNode y = x.Left;
            TreeNode b = y.Right;
            y.Parent = x.Parent;

            if (xParent.Key > y.Key)
            {
                xParent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            x.Parent = y;
            y.Right = x;
            b.Parent = x;
            x.Left = b;
        }

        private TreeNode RightAncestor(TreeNode node)
        {
            //if node is the largest element in the tree
            if (node.Parent == null)
            {
                return null;
            }

            if (node.Key < node.Parent.Key)
            {
                return node.Parent;
            }

            return RightAncestor(node.Parent);
        }

        private TreeNode LeftDescendant(TreeNode node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return LeftDescendant(node.Left);
        }
    }
}
