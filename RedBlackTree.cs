using System.Text;

// A Red-Black Tree is a type of self-balancing binary search tree where every node has a color either red or black.
public class RedBlackTree<T> : Tree<T> where T : IComparable<T>
{
    // A log to keep track of updates during RBT operations.
    public StringBuilder updatesLog { get; private set; } = new StringBuilder();

    // Enumeration for node color.
    public enum Color
    {
        Red,
        Black
    }

    // Class representing a node in the Red-Black Tree.
    public class Node : Tree<T>.Node
    {
        // The value held by the node.
        public T Value { get; set; }

        // Left and Right properties for Tree<T>.Node compatibility.
        Tree<T>.Node? Tree<T>.Node.Left => Left;
        Tree<T>.Node? Tree<T>.Node.Right => Right;

        // The left and right children of the node.
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        // The parent of the node.
        public Node? Parent { get; set; }

        // The color of the node.
        public Color NodeColor { get; set; }

        // Constructor initializes a node with a value and color.
        public Node(T value)
        {
            Value = value;
            NodeColor = Color.Red;
        }

        public bool IsRed => NodeColor == Color.Red;
    }

    // The root of the tree.
    private Node? root;

    // Root property for Tree<T> compatibility.
    Tree<T>.Node? Tree<T>.Root => root;

    // The root node of the tree.
    public Node? Root
    {
        get { return root; }
        private set { root = value; }
    }

    // Method to insert a value into the tree.
    public void Insert(T value)
    {
        updatesLog = new StringBuilder(); // initialize updatesLog to an empty string
        if (root == null) // if the tree is empty
        {
            root = new Node(value)
                { NodeColor = Color.Black }; // create a new node with the given value and set it as the root
            updatesLog.AppendLine($"Inserted {value} as root."); // add a log entry for the insertion
        }
        else // if the tree is not empty
        {
            Node currentNode = root; // start at the root
            Node newNode = new Node(value); // create a new node with the given value

            while (true) // loop until we insert the new node
            {
                int comparison = value.CompareTo(currentNode.Value); // compare the value to the current node's value
                if (comparison < 0) // if the value is less than the current node's value
                {
                    if (currentNode.Left == null) // if the current node has no left child
                    {
                        currentNode.Left = newNode; // set the new node as the current node's left child
                        newNode.Parent = currentNode; // set the current node as the new node's parent
                        updatesLog.AppendLine(
                            $"Inserted {value} as left child of {currentNode.Value}."); // add a log entry for the insertion
                        break; // exit the loop
                    }

                    currentNode = currentNode.Left; // move to the current node's left child
                }
                else if (comparison > 0) // if the value is greater than the current node's value
                {
                    if (currentNode.Right == null) // if the current node has no right child
                    {
                        currentNode.Right = newNode; // set the new node as the current node's right child
                        newNode.Parent = currentNode; // set the current node as the new node's parent
                        updatesLog.AppendLine(
                            $"Inserted {value} as right child of {currentNode.Value}."); // add a log entry for the insertion
                        break; // exit the loop
                    }

                    currentNode = currentNode.Right; // move to the current node's right child
                }
                else // if the value is equal to the current node's value
                {
                    updatesLog.AppendLine(
                        $"Value {value} already exists in the tree."); // add a log entry for the duplicate value
                    return; // exit the method
                }
            }

            FixTreeAfterInsert(newNode); // fix the tree after the insertion
        }
    }

    // This function fixes the violations caused by BST insertion.
    private void FixTreeAfterInsert(Node node)
    {
        while (node != root &&
               node.Parent!.NodeColor == Color.Red) // loop while the node's parent is red and the node is not the root
        {
            if (node.Parent == node.Parent.Parent!.Left) // if the node's parent is the left child of its grandparent
            {
                Node uncle = node.Parent.Parent.Right!; // get the node's uncle (the right child of its grandparent)
                if (uncle != null && uncle.NodeColor == Color.Red) // if the uncle is red
                {
                    updatesLog.AppendLine(
                        $"Uncle {uncle.Value} is red. Recoloring nodes."); // add a log entry for the recoloring
                    node.Parent.NodeColor = Color.Black; // set the node's parent to black
                    uncle.NodeColor = Color.Black; // set the uncle to black
                    node.Parent.Parent.NodeColor = Color.Red; // set the grandparent to red
                    node = node.Parent.Parent; // move up the tree to the grandparent
                }
                else // if the uncle is black or null
                {
                    if (node == node.Parent.Right) // if the node is the right child of its parent
                    {
                        updatesLog.AppendLine(
                            $"Node {node.Value} is a right child. Performing left rotation."); // add a log entry for the left rotation
                        node = node.Parent; // move up the tree to the node's parent
                        RotateLeft(node); // perform a left rotation on the node's parent
                    }

                    updatesLog.AppendLine(
                        $"Recoloring nodes and performing right rotation."); // add a log entry for the recoloring and right rotation
                    node.Parent!.NodeColor = Color.Black; // set the node's parent to black
                    node.Parent.Parent!.NodeColor = Color.Red; // set the grandparent to red
                    RotateRight(node.Parent.Parent); // perform a right rotation on the grandparent
                }
            }
            else // if the node's parent is the right child of its grandparent
            {
                Node uncle = node.Parent.Parent.Left!; // get the node's uncle (the left child of its grandparent)
                if (uncle != null && uncle.NodeColor == Color.Red) // if the uncle is red
                {
                    updatesLog.AppendLine(
                        $"Uncle {uncle.Value} is red. Recoloring nodes."); // add a log entry for the recoloring
                    node.Parent.NodeColor = Color.Black; // set the node's parent to black
                    uncle.NodeColor = Color.Black; // set the uncle to black
                    node.Parent.Parent.NodeColor = Color.Red; // set the grandparent to red
                    node = node.Parent.Parent; // move up the tree to the grandparent
                }
                else // if the uncle is black or null
                {
                    if (node == node.Parent.Left) // if the node is the left child of its parent
                    {
                        updatesLog.AppendLine(
                            $"Node {node.Value} is a left child. Performing right rotation."); // add a log entry for the right rotation
                        node = node.Parent; // move up the tree to the node's parent
                        RotateRight(node); // perform a right rotation on the node's parent
                    }

                    updatesLog.AppendLine(
                        $"Recoloring nodes and performing left rotation."); // add a log entry for the recoloring and left rotation
                    node.Parent!.NodeColor = Color.Black; // set the node's parent to black
                    node.Parent.Parent!.NodeColor = Color.Red; // set the grandparent to red
                    RotateLeft(node.Parent.Parent); // perform a left rotation on the grandparent
                }
            }
        }

        root!.NodeColor = Color.Black; // set the root to black
    }

    // A utility function to do left rotation.
    private void RotateLeft(Node node)
    {
        updatesLog.AppendLine($"Rotated left around {node.Value}."); // add a log entry for the rotation
        Node rightChild = node.Right!; // get the node's right child
        node.Right = rightChild!.Left; // set the node's right child to the right child's left child

        if (rightChild.Left != null) // if the right child's left child is not null
        {
            rightChild.Left.Parent = node; // set the right child's left child's parent to the node
        }

        rightChild.Parent = node.Parent; // set the right child's parent to the node's parent

        if (node.Parent == null) // if the node is the root
        {
            root = rightChild; // set the right child as the new root
        }
        else if (node == node.Parent.Left) // if the node is the left child of its parent
        {
            node.Parent.Left = rightChild; // set the right child as the new left child of the node's parent
        }
        else // if the node is the right child of its parent
        {
            node.Parent.Right = rightChild; // set the right child as the new right child of the node's parent
        }

        rightChild.Left = node; // set the node as the left child of the right child
        node.Parent = rightChild; // set the right child as the parent of the node
    }

    // A utility function to do right rotation.
    private void RotateRight(Node node)
    {
        updatesLog.AppendLine($"Rotated right around {node.Value}."); // add a log entry for the rotation
        Node leftChild = node.Left!; // get the node's left child
        node.Left = leftChild!.Right; // set the node's left child to the left child's right child

        if (leftChild.Right != null) // if the left child's right child is not null
        {
            leftChild.Right.Parent = node; // set the left child's right child's parent to the node
        }

        leftChild.Parent = node.Parent; // set the left child's parent to the node's parent

        if (node.Parent == null) // if the node is the root
        {
            root = leftChild; // set the left child as the new root
        }
        else if (node == node.Parent.Right) // if the node is the right child of its parent
        {
            node.Parent.Right = leftChild; // set the left child as the new right child of the node's parent
        }
        else // if the node is the left child of its parent
        {
            node.Parent.Left = leftChild; // set the left child as the new left child of the node's parent
        }

        leftChild.Right = node; // set the node as the right child of the left child
        node.Parent = leftChild; // set the left child as the parent of the node
    }

    // Function to determine the depth of the tree.
    public int Depth()
    {
        return Depth(Root!);
    }

    // Helper function for Depth.
    private int Depth(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        int leftDepth = Depth(node.Left!);
        int rightDepth = Depth(node.Right!);

        return Math.Max(leftDepth, rightDepth) + 1;
    }

    public void Delete(T value)
    {
        updatesLog = new StringBuilder();
        Node? nodeToDelete = FindNode(root, value);
        if (nodeToDelete == null)
        {
            updatesLog.AppendLine($"Value {value} not found for deletion.");
            return;
        }

        updatesLog.AppendLine($"Deleting node with value {value}.");
        DeleteNode(nodeToDelete);
    }

    private Node? FindNode(Node? node, T value)
    {
        if (node == null) return null;

        int comparisonResult = value.CompareTo(node.Value);
        if (comparisonResult < 0)
        {
            updatesLog.AppendLine($"Searching for {value} in the left subtree of node {node.Value}.");
            return FindNode(node.Left, value);
        }
        else if (comparisonResult > 0)
        {
            updatesLog.AppendLine($"Searching for {value} in the right subtree of node {node.Value}.");
            return FindNode(node.Right, value);
        }
        else
        {
            updatesLog.AppendLine($"Found node with value {value}.");
            return node;
        }
    }

    private void DeleteNode(Node nodeToDelete)
    {
        updatesLog.AppendLine($"Starting deletion process for node with value {nodeToDelete.Value}.");

        // if the node has two children, replace it with the maximum value from its left subtree
        if (nodeToDelete.Left != null && nodeToDelete.Right != null)
        {
            Node replacement = GetMaxNode(nodeToDelete.Left);
            updatesLog.AppendLine(
                $"Node {nodeToDelete.Value} has two children. Replacing with max value {replacement.Value} from left subtree.");
            nodeToDelete.Value = replacement.Value;
            nodeToDelete = replacement;
        }

        Node? child = nodeToDelete.Left ?? nodeToDelete.Right; // get the node's child (if it has one)

        if (nodeToDelete.IsRed) // if the node is red, replace it with its child
        {
            updatesLog.AppendLine($"Node {nodeToDelete.Value} is red. Replacing with its child.");
            ReplaceNode(nodeToDelete, child);
        }
        else if
            (child != null &&
             child.IsRed) // if the node is black and its child is red, replace it with its child and recolor it black
        {
            updatesLog.AppendLine(
                $"Node {nodeToDelete.Value} is black and has a red child. Replacing with its red child and recoloring it black.");
            child.NodeColor = Color.Black;
            ReplaceNode(nodeToDelete, child);
        }
        else // if the node is black and its child is black or null
        {
            updatesLog.AppendLine(
                $"Node {nodeToDelete.Value} is black and has no red children. Fixing tree after deletion.");
            Node? sibling = Sibling(nodeToDelete); // get the node's sibling

            if (sibling != null && sibling.IsRed) // if the sibling is red, recolor it and the parent, and rotate
            {
                updatesLog.AppendLine(
                    $"Sibling {sibling.Value} of node {nodeToDelete.Value} is red. Recoloring sibling and parent, and rotating.");
                nodeToDelete.Parent!.NodeColor = Color.Red;
                sibling.NodeColor = Color.Black;

                if (nodeToDelete == nodeToDelete.Parent.Left)
                {
                    RotateLeft(nodeToDelete.Parent);
                }
                else
                {
                    RotateRight(nodeToDelete.Parent);
                }
            }

            sibling = Sibling(nodeToDelete); // get the node's sibling again (it may have changed due to rotation)
            ReplaceNode(nodeToDelete, child); // replace the node with its child

            DeleteCase2(child); // fix the tree after deletion
        }

        updatesLog.AppendLine($"Deleted node with value {nodeToDelete.Value}.");
    }

    private Node GetMaxNode(Node node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }

        return node;
    }

    private Node? Sibling(Node node)
    {
        if (node.Parent == null) return null;

        if (node == node.Parent.Left)
        {
            return node.Parent.Right;
        }
        else
        {
            return node.Parent.Left;
        }
    }

    private void ReplaceNode(Node oldNode, Node? newNode)
    {
        // replace the old node with the new node
        if (oldNode.Parent == null) // if the old node is the root
        {
            root = newNode; // set the new node as the new root
        }
        else if (oldNode == oldNode.Parent.Left) // if the old node is the left child of its parent
        {
            oldNode.Parent.Left = newNode; // set the new node as the new left child of the old node's parent
        }
        else // if the old node is the right child of its parent
        {
            oldNode.Parent.Right = newNode; // set the new node as the new right child of the old node's parent
        }

        // update the parent of the new node
        if (newNode != null) // if the new node exists
        {
            updatesLog.AppendLine(
                $"Replaced node {oldNode.Value} with {newNode.Value}."); // add a log entry for the replacement
            newNode.Parent = oldNode.Parent; // set the new node's parent to the old node's parent
        }
        else // if the new node is null (i.e. the old node was deleted)
        {
            updatesLog.AppendLine($"Removed node {oldNode.Value}."); // add a log entry for the removal
        }
    }

    private void DeleteCase2(Node? node)
    {
        if (node == null) return; // if the node is null, return

        Node? sibling = Sibling(node); // get the node's sibling

        // if the sibling is black and has no red children
        if (sibling != null && !sibling.IsRed && (sibling.Left == null || !sibling.Left.IsRed) &&
            (sibling.Right == null || !sibling.Right.IsRed))
        {
            sibling.NodeColor = Color.Red; // recolor the sibling to red

            if (node.Parent!.IsRed) // if the node's parent is red
            {
                node.Parent.NodeColor = Color.Black; // recolor the parent to black
            }
            else // if the node's parent is black
            {
                DeleteCase2(node.Parent); // recursively fix the tree for the parent
            }
        }
        else // if the sibling is red or has a red child
        {
            DeleteCase3(node); // move to case 3
        }

        updatesLog.AppendLine($"Delete case 2 performed for node {node.Value}."); // add a log entry for the case
    }

    private void DeleteCase3(Node? node)
    {
        if (node == null) return; // if the node is null, return

        Node? sibling = Sibling(node); // get the node's sibling

        if (sibling != null && !sibling.IsRed) // if the sibling is black
        {
            if (node == node.Parent!.Left && // if the node is the left child of its parent
                (sibling.Right == null || !sibling.Right.IsRed) && // if the sibling's right child is black or null
                sibling.Left != null && sibling.Left.IsRed) // if the sibling's left child is red
            {
                sibling.NodeColor = Color.Red; // recolor the sibling to red
                sibling.Left.NodeColor = Color.Black; // recolor the sibling's left child to black
                RotateRight(sibling); // rotate the sibling to the right
            }
            else if (node == node.Parent.Right && // if the node is the right child of its parent
                     (sibling.Left == null || !sibling.Left.IsRed) && // if the sibling's left child is black or null
                     sibling.Right != null && sibling.Right.IsRed) // if the sibling's right child is red
            {
                sibling.NodeColor = Color.Red; // recolor the sibling to red
                sibling.Right.NodeColor = Color.Black; // recolor the sibling's right child to black
                RotateLeft(sibling); // rotate the sibling to the left
            }
        }

        DeleteCase4(node); // move to case 4
        updatesLog.AppendLine($"Delete case 3 performed for node {node.Value}."); // add a log entry for the case
    }

    private void DeleteCase4(Node? node)
    {
        if (node == null) return; // if the node is null, return

        Node? sibling = Sibling(node); // get the node's sibling

        if (sibling != null) // if the sibling exists
        {
            sibling.NodeColor = node.Parent!.NodeColor; // set the sibling's color to the parent's color
            node.Parent.NodeColor = Color.Black; // set the parent's color to black

            if (node == node.Parent.Left) // if the node is the left child of its parent
            {
                if (sibling.Right != null) // if the sibling's right child exists
                {
                    sibling.Right.NodeColor = Color.Black; // set the sibling's right child's color to black
                }

                RotateLeft(node.Parent); // rotate the parent to the left
            }
            else // if the node is the right child of its parent
            {
                if (sibling.Left != null) // if the sibling's left child exists
                {
                    sibling.Left.NodeColor = Color.Black; // set the sibling's left child's color to black
                }

                RotateRight(node.Parent); // rotate the parent to the right
            }
        }

        updatesLog.AppendLine($"Delete case 4 performed for node {node.Value}."); // add a log entry for the case
    }
}