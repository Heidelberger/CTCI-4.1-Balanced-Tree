using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_4._1_Balanced_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(4, 1, "Check if Binary Tree is Balanced");

            Node treeBalanced = CreateBalancedTree(7);
            PrintNodes(treeBalanced);
            if (CheckBalance(treeBalanced) == true)
                Console.WriteLine("Tree is balanced.");
            else
                Console.WriteLine("Tree is unbalanced.");

            Console.WriteLine(); Console.WriteLine(); Console.WriteLine();

            Node treeUnbalanced = CreateUnbalancedTree(7);
            PrintNodes(treeUnbalanced);
            if (CheckBalance(treeUnbalanced) == true)
                Console.WriteLine("Tree is balanced.");
            else
                Console.WriteLine("Tree is unbalanced.");
            
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// Check tree by recursing through its nodes using auxillary function GetTreeHeight()
        /// 
        /// By calling GetTreeHeight() at each level, we can discover if any branch is unbalanced
        /// 
        /// </summary>
        /// <param name="passedNode"></param>
        /// <returns></returns>
        private static bool CheckBalance(Node passedNode)
        {
            // "balanced" means: heights of 2 subtrees of any node never differ by more than one

            if (passedNode == null)
                return true;

            int heightDiff = GetTreeHeight(passedNode.child_A) - GetTreeHeight(passedNode.child_B);

            if (Math.Abs(heightDiff) > 1)
                return false;

            return ((CheckBalance(passedNode.child_A)) && (CheckBalance(passedNode.child_B)));            
        }

        /// <summary>
        /// 
        /// Recurse through all children nodes. 
        /// Return 0 on null, add 1 after each return.
        /// Return the larger of the 2.
        /// 
        /// </summary>
        /// <param name="passedNode"></param>
        /// <returns></returns>
        private static int GetTreeHeight(Node passedNode)
        {
            if (passedNode == null)
                return 0;

            return Math.Max(GetTreeHeight(passedNode.child_A), GetTreeHeight(passedNode.child_B)) + 1;
        }

        private static Node CreateUnbalancedTree(int depth)
        {
            Node root = CreateBalancedTree(depth - 2);

            Stack<Node> s = new Stack<Node>();

            Node temp = root;
            while (temp.child_A != null)
            {
                temp = temp.child_A;
            }

            //create 3 new levels under this one child node
            temp.child_A = new Node(new Node(new Node(), new Node()), new Node());

            return root;
        }

        private static Node CreateBalancedTree(int depth)
        {
            Node root = new Node();

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

            // each iteration represents 1 level in the tree
            for (int i = 1; i < depth; ++i)
            {
                Queue<Node> children = new Queue<Node>();

                while(q.Count > 0)
                {
                    Node temp = q.Dequeue();
                    temp.child_A = new Node();
                    temp.child_B = new Node();

                    children.Enqueue(temp.child_A);
                    children.Enqueue(temp.child_B);
                }

                while(children.Count > 0)
                {
                    q.Enqueue(children.Dequeue());
                }
            }

            return root;
        }


        /// <summary>
        /// 
        /// Very rough-and-dirty print of the tree. Does not account for actual position of nodes.
        /// 
        /// </summary>
        /// <param name="root"></param>
        private static void PrintNodes(Node root)
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);

            // print root node
            Console.WriteLine("X");

            Queue<Node> children = new Queue<Node>();

            // outside loop controls tree level
            while (q.Count > 0)
            {
                // inside loop controls children at current level
                while (q.Count > 0)
                {
                    Node temp = q.Dequeue();
                    if (temp.child_A != null)
                    {
                        children.Enqueue(temp.child_A);
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    if (temp.child_B != null)
                    {
                        children.Enqueue(temp.child_B);
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();

                while (children.Count > 0)
                    q.Enqueue(children.Dequeue());
            }
        }

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }

    }

    class Node
    {
        public Node child_A = null;

        public Node child_B = null;

        public Node()
        {
        }

        public Node(Node A, Node B)
        {
            child_A = A;
            child_B = B;
        }
    }
}
