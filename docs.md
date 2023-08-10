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

- **`Insert(T value)`**\
  Inserts a new value into the tree.

- **`Height(Node? node)`**\
  Returns the height of a node.

- **`GetBalance(Node? node)`**\
  Returns the balance of a node.

- **`RotateRight(Node y)`**\
  Performs a right rotation at a node.

- **`RotateLeft(Node x)`**\
  Performs a left rotation at a node.

- **`Delete(T value)`**\
  Deletes a value from the tree.

- **`GetMinNode(Node node)`**\
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

- **`Insert(T value)`**\
  Inserts a new value into the tree. If the value already exists, a log entry is generated and the method returns.

- **`FixTreeAfterInsert(Node node)`**\
  Fixes the tree after an insertion operation. It performs rotations and recolorings as necessary.

- **`RotateLeft(Node node)`**\
  Performs a left rotation at a node.

- **`RotateRight(Node node)`**\
  Performs a right rotation at a node.

- **`Depth(Node node)`**\
  Returns the depth of a node.

- **`Delete(T value)`**\
  Deletes a value from the tree.

- **`FindNode(Node? node, T value)`**\
  Helper method for finding a node with a specific value.

- **`DeleteNode(Node node)`**\
  Deletes a node from the tree.

- **`GetMinNode(Node node)`**\
  Finds the node with the minimum value in a subtree.

- **`Sibling(Node node)`**\
  Returns the sibling of a node.

- **`ReplaceNode(Node? oldNode, Node? newNode)`**\
  Replaces a node with another node.

- **`DeleteCase2(Node? node)`**\
  Called when a node is deleted from the Red-Black tree and its sibling is black with no red children.

- **`DeleteCase3(Node? node)`**\
  Called when a node is deleted from the Red-Black tree and its sibling is red or has a red child.

- **`DeleteCase4(Node? node)`**\
  Called when a node is deleted from the Red-Black tree and its sibling is black with at least one red child.

# BinarySearchTree Class Documentation

The `BinarySearchTree` class represents a Binary Search Tree (BST), a tree data structure in which each node has at most two children, referred to as the left child and the right child.

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

- **`Insert(T value)`**\
  This method inserts a new value into the tree. It uses a helper method to recursively insert the value into the tree, using the BST property that all left descendants are less than the node and all right descendants are greater.

- **`Delete(T value)`**\
  This method deletes a value from the tree. It uses a helper method to recursively delete a value from the tree, preserving the BST property.

- **`MinValue(Node node)`**\
  This helper method finds the minimum value in the subtree rooted at a given node.