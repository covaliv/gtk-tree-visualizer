using System.Text;

public class BinarySearchTree<T> : Tree<T> where T : IComparable<T>
{
    public StringBuilder updatesLog { get; private set; } = new StringBuilder();
    public class Node : Tree<T>.Node
    {
        public T Value { get; set; }
        public new Node? Left { get; set; }
        public new Node? Right { get; set; }

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
            Left = null;
            Right = null;
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
        updatesLog.AppendLine($"Inserted {value} into the tree.");
    }

    private Node Insert(Node? node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving to the left child.");
            node.Left = Insert(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving to the right child.");
            node.Right = Insert(node.Right, value);
        }

        return node;
    }

    public void Delete(T value)
    {
        updatesLog = new StringBuilder();
        root = Delete(root, value);
        updatesLog.AppendLine($"Deleted {value} from the tree.");
    }

    private Node? Delete(Node? node, T value)
    {
        if (node == null)
        {
            return node;
        }

        int comparison = value.CompareTo(node.Value);

        if (comparison < 0)
        {
            updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving to the left child.");
            node.Left = Delete(node.Left, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving to the right child.");
            node.Right = Delete(node.Right, value);
        }
        else
        {
            if (node.Left == null)
            {
                updatesLog.AppendLine($"Node {node.Value} is a leaf node or has only a right child. Replacing it with its right child.");
                return node.Right;
            }
            else if (node.Right == null)
            {
                updatesLog.AppendLine($"Node {node.Value} has only a left child. Replacing it with its left child.");
                return node.Left;
            }

            T minValue = MinValue(node.Right);
            updatesLog.AppendLine($"Node {node.Value} has two children. Replacing it with the smallest value {minValue} in its right subtree.");
            node.Value = minValue;

            node.Right = Delete(node.Right, node.Value);
        }

        return node;
    }

    private T MinValue(Node node)
    {
        T minValue = node.Value;
        while (node.Left != null)
        {
            minValue = node.Left.Value;
            node = node.Left;
        }
        return minValue;
    }
}