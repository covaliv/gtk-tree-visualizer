using System.Text;

public class AVLTree<T> : Tree<T> where T : IComparable<T>
{
    public StringBuilder updatesLog = new StringBuilder();
    public class Node : Tree<T>.Node
    {
        public T Value { get; set; }
        public new Node? Left { get; set; }
        public new Node? Right { get; set; }
        public int Height { get; set; }

        Tree<T>.Node? Tree<T>.Node.Left
        {
            get => Left;
        }

        Tree<T>.Node? Tree<T>.Node.Right
        {
            get => Right;
        }

        public Node(T value)
        {
            Value = value;
            Height = 1;
        }
    }

    private Node? root;

    Tree<T>.Node? Tree<T>.Root => root;

    public Node? Root
    {
        get { return root; }
        private set { root = value; }
    }

    public void Insert(T value)
    {
        updatesLog = new StringBuilder();
        root = Insert(root, value);
    }

    private Node Insert(Node node, T value)
    {
        if (node == null)
        {
            updatesLog.AppendLine($"Inserting {value} as a new node.");
            return new Node(value);
        }

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving left.");
            node.Left = Insert(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving right.");
            node.Right = Insert(node.Right, value);
        }
        else
        {
            updatesLog.AppendLine($"Value {value} already exists in the tree, not inserting.");
            return node;
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (value.CompareTo(node.Left.Value) < 0)
            {
                return RotateRight(node);
            }
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }

        if (balance < -1)
        {
            if (value.CompareTo(node.Right.Value) > 0)
            {
                return RotateLeft(node);
            }
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    private int Height(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    public int GetBalance(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return Height(node.Left) - Height(node.Right);
    }

    private Node RotateRight(Node y)
    {
        updatesLog.AppendLine($"Rotating right at node {y.Value}.");

        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));

        return x;
    }

    private Node RotateLeft(Node x)
    {
        updatesLog.AppendLine($"Rotating left at node {x.Value}.");

        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
        y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));

        return y;
    }

    public void Delete(T value)
    {
        updatesLog = new StringBuilder();
        root = Delete(root, value);
    }

    private Node Delete(Node node, T value)
    {
        if (node == null)
        {
            updatesLog.AppendLine($"Node with value {value} not found, nothing to delete.");
            return node;
        }

        int comparison = value.CompareTo(node.Value);

        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving left.");
            node.Left = Delete(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving right.");
            node.Right = Delete(node.Right, value);
        }
        else
        {
            if (node.Left == null || node.Right == null)
            {
                Node temp = null;
                if (temp == node.Left)
                {
                    temp = node.Right;
                }
                else
                {
                    temp = node.Left;
                }

                if (temp == null)
                {
                    node = null;
                }
                else
                {
                    node = temp;
                }
            }
            else
            {
                Node temp = GetMinNode(node.Right);
                node.Value = temp.Value;
                node.Right = Delete(node.Right, temp.Value);
            }
        }

        if (node == null)
        {
            return node;
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        int balance = GetBalance(node);

        if (balance > 1)
        {
            if (GetBalance(node.Left) >= 0)
            {
                return RotateRight(node);
            }
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }

        if (balance < -1)
        {
            if (GetBalance(node.Right) <= 0)
            {
                return RotateLeft(node);
            }
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    private Node GetMinNode(Node node)
    {
        Node current = node;

        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }
}