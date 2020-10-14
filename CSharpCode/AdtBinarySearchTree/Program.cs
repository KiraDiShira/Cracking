using AdtBinarySearchTree.AdtAVLTree;
using System;

namespace AdtBinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree tree = new AVLTree();

            /* Constructing tree given in the above figure */
            tree.Root = tree.Insert(tree.Root, 10);
            tree.Root = tree.Insert(tree.Root, 20);
            tree.Root = tree.Insert(tree.Root, 30);
            tree.Root = tree.Insert(tree.Root, 40);
            tree.Root = tree.Insert(tree.Root, 50);
            tree.Root = tree.Insert(tree.Root, 25);

            /* The constructed AVL Tree would be  
                30  
                / \  
            20 40  
            / \ \  
            10 25 50  
            */
            Console.Write("Preorder traversal" +
                            " of constructed tree is : ");
            tree.PreOrder(tree.Root);

            Console.Read();
        }
    }
}
