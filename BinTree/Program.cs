using System;

namespace BinaryTree
{
    public class Node
    {
        public int Val;
        public Node L;
        public Node R;
    }

    public class BinaryTree
    {
        private Node _root;

        public BinaryTree()
        {
            _root = null;
        }

        public void InsertNode(int key)
        {
            if (_root != null)
                InsertNode(key, _root);
            else
                _root = new Node
                {
                    Val = key,
                    L = null,
                    R = null
                };
        }

        private void InsertNode(int key, Node leaf)
        {
            while (true)
            {
                if (key < leaf.Val)
                {
                    if (leaf.L != null)
                    {
                        leaf = leaf.L;
                        continue;
                    }

                    leaf.L = new Node
                    {
                        Val = key,
                        L = null,
                        R = null
                    };
                }
                else if (key >= leaf.Val)
                {
                    if (leaf.R != null)
                    {
                        leaf = leaf.R;
                        continue;
                    }

                    leaf.R = new Node
                    {
                        Val = key,
                        R = null,
                        L = null
                    };
                }

                break;
            }
        }

        public Node SearchNode(int key)
        {
            return SearchNode(key, _root);
        }

        private static Node SearchNode(int key, Node leaf)
        {
            while (true)
            {
                if (leaf != null)
                {
                    if (key == leaf.Val)
                        return leaf;

                    leaf = key < leaf.Val ? leaf.L : leaf.R;
                }
                else
                {
                    return null;
                }
            }
        }

        public void RemoveNode(int key)
        {
            RemoveNode(_root, SearchNode(key, _root));
        }

        private static Node RemoveNode(Node root, Node removableNode)
        {
            if (root == null)
                return null;

            if (removableNode.Val < root.Val)
            {
                root.L = RemoveNode(root.L, removableNode);
            }

            if (removableNode.Val > root.Val)
            {
                root.R = RemoveNode(root.R, removableNode);
            }

            if (removableNode.Val != root.Val)
                return root;

            switch (root.L)
            {
                case null when root.R == null:
                    {
                        return null;
                    }
                case null:
                    {
                        root = root.R;
                        break;
                    }
                default:
                    {
                        if (root.R == null)
                        {
                            root = root.L;
                        }
                        else
                        {
                            var minimalNode = GetMinNode(root.R);
                            root.Val = minimalNode.Val;
                            root.R = RemoveNode(root.R, minimalNode);
                        }

                        break;
                    }
            }

            return root;
        }

        private static Node GetMinNode(Node currentNode)
        {
            while (currentNode?.L != null)
                currentNode = currentNode.L;

            return currentNode;
        }

        

        public void InOrderTravers()
        {
            InOrderTravers(_root);
            Console.WriteLine("");
        }

        private static void InOrderTravers(Node leaf)
        {
            while (true)
            {
                if (leaf != null)
                {
                    InOrderTravers(leaf.L);
                    Console.WriteLine("{0}", leaf.Val);
                    leaf = leaf.R;
                    continue;
                }

                break;
            }
        }

        
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = new BinaryTree();

            Console.WriteLine("Добавить");

            tree.InsertNode(Convert.ToInt32(41));
            tree.InsertNode(Convert.ToInt32(322));
            tree.InsertNode(Convert.ToInt32(-41));


            tree.InOrderTravers();

            Console.WriteLine("Удалить");
            tree.RemoveNode(Convert.ToInt32(3));

            Console.WriteLine("Найти");
            var temp = tree.SearchNode(Convert.ToInt32(7));
            Console.WriteLine(temp != null ? "Found!" : "Not found!");

            if (temp != null) Console.WriteLine("НАЙДЕНО");
            else Console.WriteLine("НЕ НАЙДЕНО");




            
        }
    }
}
