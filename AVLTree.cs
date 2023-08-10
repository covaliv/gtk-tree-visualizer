using System.Text;

// This class represents an AVL tree, a type of binary search tree that automatically remains balanced.
public class AVLTree<T> : Tree<T> where T : IComparable<T>
{
    // This property logs all the updates done during AVL tree operations.
    public StringBuilder updatesLog = new StringBuilder();

    // This class represents a node in the AVL tree.
    public class Node : Tree<T>.Node
    {
        // This property holds the value of the node.
        public T Value { get; set; }
        // These properties point to the left and right child nodes.
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        // This property holds the height of the node.
        public int Height { get; set; }

        Tree<T>.Node? Tree<T>.Node.Left
        {
            get => Left;
        }

        Tree<T>.Node? Tree<T>.Node.Right
        {
            get => Right;
        }

        // This constructor initializes a new node with a given value and a height of 1.
        public Node(T value)
        {
            Value = value;
            Height = 1;
        }
    }

    // This property points to the root node of the tree.
    private Node? root;

    Tree<T>.Node? Tree<T>.Root => root;

    public Node? Root
    {
        get { return root; }
        private set { root = value; }
    }

    // This method inserts a new value into the tree.
    public void Insert(T value)
    {
        updatesLog = new StringBuilder();
        updatesLog.AppendLine($"Inserting value {value} into the tree.");
        // The root node might get replaced during balancing, so we update it here.
        root = Insert(root!, value);
    }

    // This helper method inserts a new value into the tree, automatically performing rotations to keep the tree balanced.
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
            node.Left = Insert(node.Left!, value);
        }
        else if (comparison > 0)
        {
            updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving right.");
            node.Right = Insert(node.Right!, value);
        }
        else
        {
            updatesLog.AppendLine($"Value {value} already exists in the tree, not inserting.");
            return node;
        }

        // After insertion, the node's height is updated and the node's balance is checked.
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        updatesLog.AppendLine($"Updated height of node {node.Value} to {node.Height}.");
        int balance = GetBalance(node);
        updatesLog.AppendLine($"Balance of node {node.Value} is {balance}.");

        // Depending on the balance, different rotations are performed.
        if (balance > 1)
        {
            if (value.CompareTo(node.Left!.Value) < 0)
            {
                updatesLog.AppendLine($"Left-left imbalance detected at node {node.Value}. Performing right rotation.");
                return RotateRight(node);
            }
            else
            {
                updatesLog.AppendLine($"Left-right imbalance detected at node {node.Value}. Performing left-right rotation.");
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }

        if (balance < -1)
        {
            if (value.CompareTo(node.Right!.Value) > 0)
            {
                updatesLog.AppendLine($"Right-right imbalance detected at node {node.Value}. Performing left rotation.");
                return RotateLeft(node);
            }
            else
            {
                updatesLog.AppendLine($"Right-left imbalance detected at node {node.Value}. Performing right-left rotation.");
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }

        return node;
    }

    // This method returns the height of a node, treating null as having a height of 0.
    private int Height(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    // This method returns the balance of a node, defined as the height of the left child minus the height of the right child.
    public int GetBalance(Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return Height(node.Left) - Height(node.Right);
    }

    // These methods perform a right or left rotation at a node, updating the heights of the nodes and returning the new root node.
private Node RotateRight(Node y)
{
    updatesLog.AppendLine($"Rotating right at node {y.Value}.");

    Node x = y.Left!; // set x to y's left child
    Node T2 = x.Right!; // set T2 to x's right child

    // perform the rotation
    x.Right = y;
    y.Left = T2;

    // update the heights of the nodes
    y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
    x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));

    updatesLog.AppendLine($"Rotation right complete. New heights: Node {y.Value} height {y.Height}, Node {x.Value} height {x.Height}.");

    return x; // return the new root of the subtree
}

private Node RotateLeft(Node x)
{
    updatesLog.AppendLine($"Rotating left at node {x.Value}.");

    Node y = x.Right!; // set y to x's right child
    Node T2 = y.Left!; // set T2 to y's left child

    // perform the rotation
    y.Left = x;
    x.Right = T2;

    // update the heights of the nodes
    x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
    y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));

    updatesLog.AppendLine($"Rotation left complete. New heights: Node {x.Value} height {x.Height}, Node {y.Value} height {y.Height}.");

    return y; // return the new root of the subtree
}

    // This method deletes a value from the tree.
    public void Delete(T value)
    {
        updatesLog = new StringBuilder();
        updatesLog.AppendLine($"Deleting value {value} from the tree.");
        // The root node might get replaced during balancing, so we update it here.
        root = Delete(root!, value);
    }

    // This helper method deletes a value from the tree, automatically performing rotations to keep the tree balanced.
    private Node Delete(Node node, T value)
{
    if (node == null) // if the node is null, the value is not found
    {
        updatesLog.AppendLine($"Node with value {value} not found, nothing to delete.");
        return node!;
    }

    int comparison = value.CompareTo(node.Value); // compare the value to the node's value

    if (comparison < 0) // if the value is less than the node's value, move left
    {
        updatesLog.AppendLine($"Value {value} is less than {node.Value}, moving left.");
        node.Left = Delete(node.Left!, value);
    }
    else if (comparison > 0) // if the value is greater than the node's value, move right
    {
        updatesLog.AppendLine($"Value {value} is greater than {node.Value}, moving right.");
        node.Right = Delete(node.Right!, value);
    }
    else // if the value is equal to the node's value, delete the node
    {
        if (node.Left == null || node.Right == null) // if the node has one or no children
        {
            Node temp = null!;
            if (temp == node.Left) // if the node has a left child
            {
                temp = node.Right!; // set temp to the right child
            }
            else // if the node has a right child or no children
            {
                temp = node.Left!; // set temp to the left child
            }

            if (temp == null) // if the node has no children
            {
                node = null!; // set the node to null
            }
            else // if the node has one child
            {
                node = temp; // set the node to the child
            }
        }
        else // if the node has two children
        {
            Node temp = GetMinNode(node.Right); // get the minimum node in the right subtree
            node.Value = temp.Value; // set the node's value to the minimum node's value
            node.Right = Delete(node.Right, temp.Value); // delete the minimum node
        }
    }

    if (node == null) // if the node is null, return null
    {
        return node!;
    }

    // After deletion, we update the height and balance of the node.
    // Depending on the balance, different rotations are performed.
    node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
    updatesLog.AppendLine($"Updated height of node {node.Value} to {node.Height}.");
    int balance = GetBalance(node);
    updatesLog.AppendLine($"Balance of node {node.Value} is {balance}.");

    if (balance > 1) // if the node is left-heavy
    {
        if (GetBalance(node.Left) >= 0) // if the node's left child is left-heavy or balanced
        {
            updatesLog.AppendLine($"Left-heavy imbalance detected at node {node.Value}. Performing right rotation.");
            return RotateRight(node); // perform a right rotation
        }
        else // if the node's left child is right-heavy
        {
            updatesLog.AppendLine($"Left-right imbalance detected at node {node.Value}. Performing left-right rotation.");
            node.Left = RotateLeft(node.Left!); // perform a left rotation on the node's left child
            return RotateRight(node); // perform a right rotation on the node
        }
    }

    if (balance < -1) // if the node is right-heavy
    {
        if (GetBalance(node.Right) <= 0) // if the node's right child is right-heavy or balanced
        {
            updatesLog.AppendLine($"Right-heavy imbalance detected at node {node.Value}. Performing left rotation.");
            return RotateLeft(node); // perform a left rotation
        }
        else // if the node's right child is left-heavy
        {
            updatesLog.AppendLine($"Right-left imbalance detected at node {node.Value}. Performing right-left rotation.");
            node.Right = RotateRight(node.Right!); // perform a right rotation on the node's right child
            return RotateLeft(node); // perform a left rotation on the node
        }
    }

    return node; // return the node
}

    // This helper method finds the node with the minimum value in the subtree rooted at a given node.
    private Node GetMinNode(Node node)
    {
        Node current = node;

        while (current.Left != null)
        {
            updatesLog.AppendLine($"Moving left to find the minimum node. Current node value: {current.Value}.");
            current = current.Left;
        }

        updatesLog.AppendLine($"Minimum node found. Node value: {current.Value}.");
        return current;
    }
}