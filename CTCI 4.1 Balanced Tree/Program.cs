using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CTCI_4._1_Balanced_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(4, 1, "Check if Binary Tree is Balanced");

            Node treeBalanced = CreateBalancedTree(22);
            //PrintNodes(treeBalanced);

            bool result = false;

            Stopwatch sw = new Stopwatch();
            
            sw.Restart();
            result = CheckBalance_N(treeBalanced);
            sw.Stop();
            if (result == true)
                Console.WriteLine("O(N)     Tree is balanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));
            else
                Console.WriteLine("O(N)     Tree is unbalanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));

            sw.Restart();
            result = CheckBalance_NlogN(treeBalanced);
            sw.Stop();            
            if (result == true)
                Console.WriteLine("O(NlogN) Tree is balanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));
            else
                Console.WriteLine("O(NlogN) Tree is unbalanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));

            Console.WriteLine();

            Node treeUnbalanced = CreateUnbalancedTree(treeBalanced);
            //PrintNodes(treeUnbalanced);

            sw.Restart();
            result = CheckBalance_N(treeUnbalanced);
            sw.Stop();
            if (result)
                Console.WriteLine("O(N)     Tree is balanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));
            else
                Console.WriteLine("O(N)     Tree is unbalanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));

            sw.Restart();
            result = CheckBalance_NlogN(treeUnbalanced);
            sw.Stop();
            if (result == true)
                Console.WriteLine("O(NlogN) Tree is balanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));
            else
                Console.WriteLine("O(NlogN) Tree is unbalanced. Elapsed time: " + sw.Elapsed.ToString("mm\\:ss\\.ffff"));

            Console.ReadLine();
        }

        private static bool CheckBalance_N(Node passedNode)
        {
            if (CheckHeight_N(passedNode) == -1)
                return false;

            return true;
        }

        /// <summary>
        /// 
        /// Recursively check height on every node
        /// Quit immediately if any branch is unbalanced
        /// 
        /// Runs in O(N): must touch every node worst-case
        /// 
        /// Requires O(H) memory, where H is height of tree 
        /// Every level requires a recursive call, therefore a frame on the stack.
        /// 
        /// </summary>
        /// <param name="passedNode"></param>
        /// <returns></returns>
        private static int CheckHeight_N(Node passedNode)
        {
            // base case
            if (passedNode == null)
                return 0;

            // check _A branch
            int height_A = CheckHeight_N(passedNode.child_A);
            if (height_A == -1)
                return -1;

            // check _B branch
            int height_B = CheckHeight_N(passedNode.child_B);
            if (height_B == -1)
                return -1;

            // compare _A and _B branches
            int height_diff = Math.Abs(height_A - height_B);
            if (height_diff > 1)
                return -1;

            // return height of current location
            return Math.Max(height_A, height_B) + 1;
        }

        /// <summary>
        /// 
        /// Check tree by recursing through its nodes using auxillary function GetTreeHeight()
        /// 
        /// By calling GetTreeHeight() at each level, we can discover if any branch is unbalanced
        /// 
        /// Inefficient because CheckBalance_NlogN() calls GetTreeHeight_NlogN() on child nodes, but then
        /// recurses on child nodes, again calling GetTreeHeight_NlogN() on child nodes.
        /// 
        /// Runs in O(NlogN) time.  Each child node is hit once for every node above it.
        /// 
        /// 
        /// </summary>
        /// <param name="passedNode"></param>
        /// <returns></returns>
        private static bool CheckBalance_NlogN(Node passedNode)
        {
            // "balanced" means: heights of 2 subtrees of any node never differ by more than one

            if (passedNode == null)
                return true;

            int heightDiff = GetTreeHeight_NlogN(passedNode.child_A) - GetTreeHeight_NlogN(passedNode.child_B);

            if (Math.Abs(heightDiff) > 1)
                return false;

            return ((CheckBalance_NlogN(passedNode.child_A)) && (CheckBalance_NlogN(passedNode.child_B)));            
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
        private static int GetTreeHeight_NlogN(Node passedNode)
        {
            if (passedNode == null)
                return 0;

            return Math.Max(GetTreeHeight_NlogN(passedNode.child_A), GetTreeHeight_NlogN(passedNode.child_B)) + 1;
        }

        private static Node CreateUnbalancedTree(int depth)
        {
            Node root = CreateBalancedTree(depth);

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

        private static Node CreateUnbalancedTree(Node passedNode)
        {
            if (passedNode == null)
                passedNode = CreateBalancedTree(1);

            Console.WriteLine("Adding 5 nodes to unbalance the tree.");

            Stack<Node> s = new Stack<Node>();

            // Add nodes in the middle of the tree
            Node temp = passedNode.child_B ?? passedNode;
            while (temp.child_A != null)
            {
                temp = temp.child_A;
            }

            //create 3 new levels under this one child node
            temp.child_A = new Node(new Node(new Node(), new Node()), new Node());

            return passedNode;
        }

        private static Node CreateBalancedTree(int depth)
        {
            Console.Write("Creating tree: " + depth + " levels = " + Math.Pow(2.0, depth) + " nodes.");

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

            Console.Write("...done.");
            Console.WriteLine();

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
