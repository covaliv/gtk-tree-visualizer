# Tree Visualizer


## Description

Tree Visualizer is a C# application that allows users to visualize and interact with different types of binary trees. The supported tree types are Red-Black, AVL, and Binary Search Tree (BST), however other types of trees can be added easily. Users can insert and delete nodes, perform different types of traversals, and visualize the structure of the tree.

The main purpose of this project is to help students learn about binary trees and their properties. The application can be used to demonstrate how different types of trees work and how they differ from each other. The application can also be used to demonstrate how different types of tree traversals work.

## Screenshots

[![Screenshot 1](https://i.postimg.cc/7ZNVvvC6/image-2023-06-23-22-47-00.png)](https://postimg.cc/SnJ88vfF)
[![Screenshot 2](https://i.postimg.cc/fbTvDHXv/image-2023-06-23-22-47-15.png)](https://postimg.cc/FY8SxVTf)
[![Screenshot 3](https://i.postimg.cc/Sx4rtQx3/image-2023-06-23-22-48-22.png)](https://postimg.cc/ZCfpWS3x)

## Features

- **Tree Type Selection**: Users can select the type of tree they want to interact with (Red-Black, AVL, or BST).
- **Node Insertion**: Users can insert nodes into the tree either manually or randomly. For random insertion, users can specify the lower and upper bounds for the random values.
- **Node Deletion**: Users can delete a random or a specific node from the tree.
- **Tree Traversal**: Users can perform Inorder, Preorder, or Postorder traversal on the tree and view the result in a dialog box.
- **Tree Visualization**: The structure of the tree is visualized and updated in real-time as nodes are inserted or deleted.
- **Tree reset**: Users can reset the tree to its initial state.
- **Node Validation**: Users are prevented from inserting duplicate nodes into the tree.
- **Action Explanation**: Users can view a brief explanation of the action they performed on the tree.

## Dependencies
- .NET Core 6.0+
- GTKSharp 3.0


## Installation
- Install .NET Core 6.0+ [from here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Install GTKSharp 3.0 [from here](https://www.mono-project.com/download/stable/)
- Clone the repository
- Navigate to the cloned directory
- Run `dotnet run` to start the application


## Usage

1. Select the type of tree you want to interact with from the dropdown menu.
2. To insert a node, enter a value in the text box and click the 'Insert Node' button. To insert a random node, enter the lower and upper bounds in the respective text boxes and click the 'Insert Random Node' button.
3. To delete a node, enter the value of the node in the text box and click the 'Delete Node' button. To delete a random node, simply click the 'Delete Random Node' button.
4. To perform a traversal, select the type of traversal from the dropdown menu and click the 'Show Traversal' button. The result will be displayed in a dialog box.
5. To reset the tree, click the 'Reset Tree' button.
6. To view an explanation of the last action you performed on the tree, click the 'Show Explanation' button, a dialog box will appear with the explanation.

## Contributing

Contributions are welcome! Please feel free to submit a pull request.