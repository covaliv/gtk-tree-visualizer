# AVLTree Class Documentation

## Properties

1. **`updatesLog`**\
   Log of all updates during AVL tree operations.

1. **`root`**\
   Root node of the tree.

1. **`Root`**\
   Getter and setter for the root node.

## Inner Node Class

### Properties

1. **`Value`**\
   Node's value.

1. **`Left`** and **`Right`**\
   Left and right child nodes.

1. **`Height`**\
   Node's height.

### Constructor

- **`Node(T value)`**\
  Initializes a new node with a value and a height of 1.

## Methods

1. **`Insert(T value)`**\
   Inserts a new value into the tree.

1. **`Height(Node? node)`**\
   Returns the height of a node.

1. **`GetBalance(Node? node)`**\
   Returns the balance of a node.

1. **`RotateRight(Node y)`**\
   Performs a right rotation at a node.

1. **`RotateLeft(Node x)`**\
   Performs a left rotation at a node.

1. **`Delete(T value)`**\
   Deletes a value from the tree.

1. **`GetMinNode(Node node)`**\
   Finds the node with the minimum value in a subtree.

---

# RedBlackTree Class Documentation

## Properties

1. **`updatesLog`**\
   Log of all updates during Red-Black tree operations.

1. **`root`**\
   Root node of the tree.

1. **`Root`**\
   Getter and setter for the root node.

## Inner Node Class

### Properties

1. **`Value`**\
   Node's value.

1. **`Left`** and **`Right`**\
   Left and right child nodes.

1. **`Parent`**\
   Parent node.

1. **`NodeColor`**\
   Node's color.

1. **`IsRed`**\
   Boolean indicating if node's color is red.

### Constructor

- **`Node(T value)`**\
  Initializes a new node with a value and a color.

## Enumerations

- **`Color`**\
  Enumeration for node colors, can be either `Red` or `Black`.

## Methods

1. **`Insert(T value)`**\
   Inserts a new value into the tree. If the value already exists, a log entry is generated and the method returns.

1. **`FixTreeAfterInsert(Node node)`**\
   Fixes the tree after an insertion operation. It performs rotations and recolorings as necessary.

1. **`RotateLeft(Node node)`**\
   Performs a left rotation at a node.

1. **`RotateRight(Node node)`**\
   Performs a right rotation at a node.

1. **`Depth(Node node)`**\
   Returns the depth of a node.

1. **`Delete(T value)`**\
   Deletes a value from the tree.

1. **`FindNode(Node? node, T value)`**\
   Helper method for finding a node with a specific value.

1. **`DeleteNode(Node node)`**\
   Deletes a node from the tree.

1. **`GetMinNode(Node node)`**\
   Finds the node with the minimum value in a subtree.

1. **`Sibling(Node node)`**\
   Returns the sibling of a node.

1. **`ReplaceNode(Node? oldNode, Node? newNode)`**\
   Replaces a node with another node.

1. **`DeleteCase2(Node? node)`**\
   Called when a node is deleted from the Red-Black tree and its sibling is black with no red children.

1. **`DeleteCase3(Node? node)`**\
   Called when a node is deleted from the Red-Black tree and its sibling is red or has a red child.

1. **`DeleteCase4(Node? node)`**\
   Called when a node is deleted from the Red-Black tree and its sibling is black with at least one red child.

# BinarySearchTree Class Documentation

The `BinarySearchTree` class represents a Binary Search Tree (BST), a tree data structure in which each node has at most
two children, referred to as the left child and the right child.

## Properties

1. **`updatesLog`**\
   This property holds a log of updates made during BST operations. It is of type `StringBuilder`.

1. **`Root`**\
   This property points to the root node of the tree. It is of type `Node`.

## Inner Node Class

### Properties

1. **`Value`**\
   This property holds the value of the node. It is of type `T`.

1. **`Left`** and **`Right`**\
   These properties point to the left and right child nodes respectively. They are of type `Node`.

### Constructor

- **`Node(T value)`**\
  This constructor initializes a new node with a given value and sets both child nodes to null.

## Methods

1. **`Insert(T value)`**\
   This method inserts a new value into the tree. It uses a helper method to recursively insert the value into the tree,
   using the BST property that all left descendants are less than the node and all right descendants are greater.

1. **`Delete(T value)`**\
   This method deletes a value from the tree. It uses a helper method to recursively delete a value from the tree,
   preserving the BST property.

1. **`MinValue(Node node)`**\
   This helper method finds the minimum value in the subtree rooted at a given node.

# TreeVisualizer Class Documentation

The `TreeVisualizer` class extends the `Gtk.Window` class and represents a graphical user interface for visualizing and
interacting with different types of trees: Red-Black, AVL, and Binary Search Tree.

## Properties

1. **`currentTreeType`**\
   This property holds the current type of tree being displayed and interacted with. It is of type `TreeType`.

1. **`rbTree`**, **`avlTree`**, **`bsTree`**\
   These properties are instances of `RedBlackTree<int>`, `AVLTree<int>`, and `BinarySearchTree<int>` respectively. They
   represent the trees that are visualized and manipulated in the display.

1. **`next_x`**\
   This property is used to calculate the x-coordinate for the next node in the tree. It is of type `double`.

1. **`nodeDistance`**\
   This property defines the distance between nodes in the visual representation of the tree. It is of type `double`.

1. **`random`**\
   This property is an instance of `System.Random` used to generate random numbers for node values.

1. **`nodeValueEntry`**, **`lowerBoundEntry`**, **`upperBoundEntry`**\
   These properties are instances of `Gtk.Entry` used to receive user input for node values and range bounds.

## Methods

1. **`public TreeVisualizer()`**\
   The constructor initializes the window with title "Tree Visualizer", sets up the layout, creates and links user
   interactive elements (buttons, entries, combo boxes), and initializes the trees.

1. **`private void ResetButton_Clicked(object sender, EventArgs e)`**\
   This method is an event handler that resets the current tree to an empty state when the "Reset Tree" button is
   clicked.

1. **`private void TreeTypeComboBox_Changed(object sender, EventArgs e)`**\
   This method is an event handler that changes the current tree type according to the user's selection in the tree type
   combo box.

1. **`private void ShowUpdatesButton_Clicked(object sender, EventArgs e)`**\
   This method is an event handler that displays the log of updates made to the current tree when the "Show Explanation"
   button is clicked.

1. **`private int? ShowInputDialog()`**\
   This method displays a dialog box with a text field for the user to enter an integer, which is parsed and returned.
   If the user clicks "Cancel" or enters a non-integer value, the method returns null.

1. **`private void InsertRandomButton_Clicked(object sender, EventArgs e)`**

   This method is an event handler that inserts a random value into the current tree when the "Insert Random" button is
   clicked. The random value is generated within the range specified by the user through the lower and upper bound entry
   fields. If the input is invalid or the lower bound is greater than the upper bound, an error message is displayed.

1. **`private void ShowTraversalButton_Clicked(object sender, EventArgs e, ComboBoxText traversalComboBox)`**

   This method is an event handler that performs a tree traversal when the "Show Traversal" button is clicked. The type
   of traversal is selected by the user from the traversalComboBox. The result of the traversal is displayed in a
   message dialog.

1. **`private void PerformTraversal(dynamic node, List<int> traversalResult, string traversalType)`**

   This is a helper method that performs a tree traversal based on the traversal type specified. This method is
   recursive and is designed to work with any type of binary tree. It updates a list with the traversal result which can
   be used to display or further process the traversal.

1. **`private void DeleteRandomButton_Clicked(object sender, EventArgs e)`**

   This method is an event handler that deletes a random node from the current tree when the "Delete Random" button is
   clicked. If the tree is empty, an error message is displayed.

1. **`private void CreateMessageDialog(string message)`**

   This is a helper method that creates a message dialog with a specified message. The message dialog includes a "OK"
   button and the message text is displayed within a `ScrolledWindow` for better readability when the message is long.

1. `GetRandomValueFromTree(dynamic node)`
   This method retrieves a random value from a tree. It takes a dynamic type node as an argument. If the node is null,
   an ArgumentException is thrown. The method traverses the tree in random directions (left, right, or current), until
   it hits a leaf node or decides to stop at the current node, at which point it returns the node's value.

1. `AddNodeButton_Clicked(object sender, EventArgs e)`
   This event handler is triggered when the "Add Node" button is clicked. It tries to parse the text
   from `nodeValueEntry.Text` to an integer and inserts it into the currently selected tree (`rbTree`, `avlTree`,
   or `bsTree`). After successful insertion, the tree is redrawn and the text box is cleared. If the input is invalid,
   it displays a message dialog with an error message.

1. `DeleteNodeButton_Clicked(object sender, EventArgs e)`
   This event handler is triggered when the "Delete Node" button is clicked. It attempts to parse the text
   from `nodeValueEntry.Text` to an integer and removes it from the currently selected tree (`rbTree`, `avlTree`,
   or `bsTree`). After successful deletion, the tree is redrawn and the text box is cleared. If the input is invalid, it
   displays a message dialog with an error message.

1. `OnDraw(object o, DrawnArgs args)`
   This method handles the drawing of the tree. It sets the background color to white and sets the line width. It
   calculates the total width of the tree and the x-offset for drawing the tree. It then calls the `Draw()` method to
   draw the tree.

1. `CalculateTreeWidth(dynamic node)`
   This method calculates the width of a given tree. It uses a recursive approach, where the width of a node is
   considered 1 and the width of a tree is the maximum width of its left and right subtrees plus 1.

1. `CalculateTotalTreeWidth(dynamic node, double depth)`
   This method calculates the total width of a given tree. It uses a recursive approach, where the width of a node is
   considered 1 and the total width of a tree is the sum of the widths of its left and right subtrees and the width of
   the current node times the distance between nodes.

1. `Draw(Context cr, dynamic node, double depth)`
   This method draws the given tree node and its child nodes recursively. It takes into account the depth and balance
   factor (in case of AVL trees) of each node.

1. `DrawLine(Context cr, double x1, double y1, double x2, double y2, double radius)`
   This method draws a line between two points, adjusting the start and end points to be on the edge of the node
   circles.

1. `DrawCircle(Context cr, double x, double y, double radius, string text, bool isRed = false, int balanceFactor = 0)`
   This method draws a circle which represents a node in the tree. It takes into account the type of the tree and
   adjusts the color accordingly. It also draws the value and, in case of AVL trees, the balance factor of the node.

1. `Main()`
   The main method initializes the application and starts the event loop.

## Note

- The `dynamic` keyword is used in the `PerformTraversal` method to allow for different types of tree
  nodes (`RedBlackTreeNode`, `AVLTreeNode`, `BSTreeNode`) to be passed in. This improves the flexibility and code reuse
  but it comes with a trade-off of type safety and performance.

## Nested Enum

- **`TreeType`**\
  This enum represents the types of trees that can be visualized: `RedBlack`, `AVL`, and `BinarySearchTree`.