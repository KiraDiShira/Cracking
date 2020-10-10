using System;

namespace AdtBinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            TreeNode root = new TreeNode()
            {
                Key = 9,
                Parent = null
            };

            TreeNode one = new TreeNode()
            {
                Key = 1,
                               
            };

            TreeNode six = new TreeNode()
            {
                Key = 6,
               
            };

            TreeNode seven = new TreeNode()
            {
                Key = 7,
               
            };

            TreeNode eight = new TreeNode()
            {
                Key = 8,
               
            };

            root.Left = one;
            one.Parent = root;
            one.Right = six;
            six.Parent = one;
            six.Right = eight;
            eight.Parent = six;
            eight.Left = seven;
            seven.Parent = eight;



           

            BinarySearchTreeOperations binarySearchTreeOperations = new BinarySearchTreeOperations();

            //binarySearchTreeOperations.Insert(4, root);
            //binarySearchTreeOperations.Insert(2, root);
            //binarySearchTreeOperations.Insert(6, root);
            //binarySearchTreeOperations.Insert(1, root);
            //binarySearchTreeOperations.Insert(3, root);

            binarySearchTreeOperations.Delete(root);
         

            binarySearchTreeOperations.RotateRight(binarySearchTreeOperations.Find(4, root));

            Console.WriteLine("Hello World!");
        }
    }
}
