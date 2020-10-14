using System.Collections.Generic;
using System.Text;

namespace AdtBinarySearchTree.AdtAVLTree
{
    public class Node
    {
        public int key, height;
        public Node left, right;

        public Node(int d)
        {
            key = d;
            height = 1;
        }
    }
}
