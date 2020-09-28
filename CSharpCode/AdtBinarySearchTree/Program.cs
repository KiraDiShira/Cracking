using System;

namespace AdtBinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode()
            {
                Key = 5,
                Parent = null
            };

            BinarySearchTreeOperations binarySearchTreeOperations = new BinarySearchTreeOperations();

            binarySearchTreeOperations.Insert(4, root);
            binarySearchTreeOperations.Insert(2, root);
            binarySearchTreeOperations.Insert(6, root);
            binarySearchTreeOperations.Insert(1, root);
            binarySearchTreeOperations.Insert(3, root);
    

         

            binarySearchTreeOperations.RotateRight(binarySearchTreeOperations.Find(4, root));

            Console.WriteLine("Hello World!");
        }
    }
}
