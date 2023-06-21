﻿using Gtk;
using Cairo;
using System;

public class TreeVisualizer : Window
{
    private enum TreeType
    {
        RedBlack,
        AVL
    }

    private RedBlackTree<int> rbTree;
    private AVLTree<int> avlTree;
    private TreeType currentTreeType;
    private double next_x = 0;
    private double nodeDistance = 23; // Distance between nodes
    private Random random = new Random();
    private Entry nodeValueEntry;
    private Entry lowerBoundEntry;
    private Entry upperBoundEntry;


    public TreeVisualizer() : base("Tree Visualizer")
    {
        rbTree = new RedBlackTree<int>();
        avlTree = new AVLTree<int>();
        currentTreeType = TreeType.RedBlack;

        // Create a VBox layout
        Box vbox = new Box(Orientation.Vertical, 10);

        // Create a new DrawingArea to display the tree
        DrawingArea drawingArea = new DrawingArea();

        // Connect the Draw event to the OnDraw method
        drawingArea.Drawn += OnDraw;

        // Add the DrawingArea to the VBox
        vbox.PackStart(drawingArea, true, true, 0);

        // Create a button box for the buttons
        Box buttonBox = new Box(Orientation.Horizontal, 10);
        buttonBox.Margin = 5;
        vbox.PackStart(buttonBox, false, false, 0);

        // Create the Add Node button
        Button addNodeButton = new Button("Add Node");
        addNodeButton.Clicked += AddNodeButton_Clicked;
        buttonBox.Add(addNodeButton);

        // Create a new Entry for the node value
        nodeValueEntry = new Entry();
        nodeValueEntry.PlaceholderText = "Enter node value";
        buttonBox.Add(nodeValueEntry);
        // Create the Delete Node button
        Button deleteNodeButton = new Button("Delete Node");
        deleteNodeButton.Clicked += DeleteNodeButton_Clicked;
        buttonBox.Add(deleteNodeButton);

        // Create a new Entry for the lower bound
        lowerBoundEntry = new Entry();
        lowerBoundEntry.PlaceholderText = "Enter lower bound";
        buttonBox.Add(lowerBoundEntry);

        // Create a new Entry for the upper bound
        upperBoundEntry = new Entry();
        upperBoundEntry.PlaceholderText = "Enter upper bound";
        buttonBox.Add(upperBoundEntry);


        // Create the Insert Random Value button
        Button insertRandomButton = new Button("Insert Random Value");
        insertRandomButton.Clicked += InsertRandomButton_Clicked;
        buttonBox.Add(insertRandomButton);

        // Create the Delete Random Value button
        Button deleteRandomButton = new Button("Delete Random Value");
        deleteRandomButton.Clicked += DeleteRandomButton_Clicked;
        buttonBox.Add(deleteRandomButton);

        Button resetButton = new Button("Reset Tree");
        resetButton.Clicked += ResetButton_Clicked;
        buttonBox.Add(resetButton);

        // Create a combo box for the tree type
        ComboBoxText treeTypeComboBox = new ComboBoxText();
        treeTypeComboBox.AppendText("Red-Black Tree");
        treeTypeComboBox.AppendText("AVL Tree");
        treeTypeComboBox.Active = 0; // Set Red-Black Tree as the default
        treeTypeComboBox.Changed += TreeTypeComboBox_Changed;
        buttonBox.Add(treeTypeComboBox);

        // Add the VBox to the window
        Add(vbox);

        // Set the size of the window as the maximum size of the screen
        SetDefaultSize(1366, 768);
        ShowAll();
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        if (currentTreeType == TreeType.RedBlack)
        {
            rbTree = new RedBlackTree<int>();
        }
        else
        {
            avlTree = new AVLTree<int>();
        }
        QueueDraw();
    }


    private void TreeTypeComboBox_Changed(object sender, EventArgs e)
    {
        ComboBoxText comboBox = (ComboBoxText)sender;
        currentTreeType = (TreeType)comboBox.Active;
        QueueDraw();
    }

    private int? ShowInputDialog()
    {
        int? result = null;

        using (Dialog dialog = new Dialog("Add Node", this, DialogFlags.Modal))
        {
            dialog.AddButton("OK", ResponseType.Ok);
            dialog.AddButton("Cancel", ResponseType.Cancel);

            Entry entry = new Entry();
            entry.ActivatesDefault = true;
            dialog.ContentArea.PackStart(entry, true, true, 0);
            dialog.DefaultResponse = ResponseType.Ok;

            dialog.ShowAll();
            int response = dialog.Run();

            if (response == (int)ResponseType.Ok)
            {
                if (int.TryParse(entry.Text, out int value))
                {
                    result = value;
                }
            }
        }

        return result;
    }

    private void InsertRandomButton_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(lowerBoundEntry.Text, out int lowerBound) && int.TryParse(upperBoundEntry.Text, out int upperBound))
        {
            if (lowerBound <= upperBound)
            {
                int randomValue = random.Next(lowerBound, upperBound + 1);
                if (currentTreeType == TreeType.RedBlack)
                {
                    rbTree.Insert(randomValue);
                }
                else
                {
                    avlTree.Insert(randomValue);
                }
                QueueDraw();
            }
            else
            {
                Console.WriteLine("Invalid range. Lower bound should be less than or equal to the upper bound.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter valid integers for the lower and upper bounds.");
        }
    }

    private void DeleteRandomButton_Clicked(object sender, EventArgs e)
    {
        if (rbTree.Root != null)
        {
            int randomValue = GetRandomValueFromTree(rbTree.Root);
            rbTree.Delete(randomValue);
            avlTree.Delete(randomValue);
            QueueDraw();
        }
        else
        {
            Console.WriteLine("The tree is empty.");
        }
    }

    private int GetRandomValueFromTree(RedBlackTree<int>.Node node)
    {
        if (node == null)
        {
            throw new ArgumentException("The input node should not be null.");
        }

        RedBlackTree<int>.Node currentNode = node;
        while (true)
        {
            int decision = random.Next(3);

            switch (decision)
            {
                case 0: // Go to the left child
                    if (currentNode.Left != null)
                    {
                        currentNode = currentNode.Left;
                    }
                    else
                    {
                        return currentNode.Value;
                    }
                    break;
                case 1: // Go to the right child
                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        return currentNode.Value;
                    }
                    break;
                default: // Return current node value
                    return currentNode.Value;
            }
        }
    }

    private void AddNodeButton_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(nodeValueEntry.Text, out int nodeValue))
        {
            if (currentTreeType == TreeType.RedBlack)
            {
                rbTree.Insert(nodeValue);
            }
            else
            {
                avlTree.Insert(nodeValue);
            }
            QueueDraw();
            nodeValueEntry.Text = ""; // Clear the text box after successful insertion
        }
        else
        {
            // Show an error message or handle the invalid input in your preferred way
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }
    }
    private void DeleteNodeButton_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(nodeValueEntry.Text, out int nodeValue))
        {
            if (currentTreeType == TreeType.RedBlack)
            {
                rbTree.Delete(nodeValue);
            }
            else
            {
                avlTree.Delete(nodeValue);
            }
            QueueDraw();
            nodeValueEntry.Text = ""; // Clear the text box after successful deletion
        }
        else
        {
            // Show an error message or handle the invalid input in your preferred way
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }
    }

    void OnDraw(object o, DrawnArgs args)
    {
        var cr = args.Cr;

        // Set background color to white
        cr.SetSourceRGB(1, 1, 1);
        cr.Paint();

        // Set line width
        cr.LineWidth = 2.0;

        // Calculate the total width of the tree
        double treeWidth = currentTreeType == TreeType.RedBlack ? CalculateTotalTreeWidth(rbTree.Root, 3) : CalculateTotalTreeWidth(avlTree.Root, 3);
        System.Console.WriteLine("Tree width: " + treeWidth);

        // Get the window width
        double windowWidth = this.Allocation.Width;

        // Calculate the starting xOffset based on the window width
        double xOffset = (windowWidth - treeWidth) / 2 - nodeDistance / 2;

        // Start drawing from the middle and add an offset
        next_x = xOffset / nodeDistance;

        // Start drawing from root
        if (currentTreeType == TreeType.RedBlack)
        {
            Draw(cr, rbTree.Root, 3);
        }
        else
        {
            Draw(cr, avlTree.Root, 3);
        }
    }


    double CalculateTreeWidth(dynamic node)
    {
        if (node == null)
        {
            return 0;
        }

        return 1 + Math.Max(CalculateTreeWidth(node.Left), CalculateTreeWidth(node.Right));
    }

    double CalculateTotalTreeWidth(dynamic node, double depth)
    {
        if (node == null)
        {
            return 0;
        }

        double width = 1.0; // Width of the current node
        double leftWidth = CalculateTotalTreeWidth(node.Left, depth + 1.5);
        double rightWidth = CalculateTotalTreeWidth(node.Right, depth + 1.5);

        return width * nodeDistance + leftWidth + rightWidth;
    }

    double Draw(Context cr, dynamic node, double depth)
    {
        if (node == null)
        {
            return 0;
        }

        double left_x = 0, right_x = 0;

        if (node.Left != null)
        {
            left_x = Draw(cr, node.Left, depth + 1.5);
            DrawLine(cr, next_x * nodeDistance, depth * nodeDistance, left_x * nodeDistance, (depth + 1.5) * nodeDistance, 17);
        }

        double my_x = next_x++;

        bool isRed = node.GetType() == typeof(RedBlackTree<int>.Node) ? node.IsRed : false;

        if (node.Right != null)
        {
            right_x = Draw(cr, node.Right, depth + 1.5);
            DrawLine(cr, my_x * nodeDistance, depth * nodeDistance, right_x * nodeDistance, (depth + 1.5) * nodeDistance, 17);
        }

        int balanceFactor = 0;
        if (currentTreeType == TreeType.AVL)
        {
            balanceFactor = avlTree.GetBalance(node);
        }
        DrawCircle(cr, my_x * nodeDistance, depth * nodeDistance, 17, node.Value.ToString(), isRed, balanceFactor);

        return my_x;
    }
    void DrawLine(Context cr, double x1, double y1, double x2, double y2, double radius)
    {
        // Calculate the angle of the line
        double angle = Math.Atan2(y2 - y1, x2 - x1);

        // Adjust the start point to be on the edge of the circle
        x1 = x1 + Math.Cos(angle) * radius;
        y1 = y1 + Math.Sin(angle) * radius;

        // Adjust the end point to be on the edge of the circle
        x2 = x2 - Math.Cos(angle) * radius;
        y2 = y2 - Math.Sin(angle) * radius;

        cr.MoveTo(x1, y1);
        cr.LineTo(x2, y2);
        cr.SetSourceRGB(0, 0, 0); // Set color to black
        cr.Stroke();
        cr.NewPath(); // Reset the current point
    }
    void DrawCircle(Context cr, double x, double y, double radius, string text, bool isRed = false, int balanceFactor = 0)
    {
        switch (currentTreeType)
        {
            case TreeType.RedBlack:
                cr.SetSourceRGB(isRed ? 1 : 0, 0, 0); // Red color for Red-Black Tree red nodes, black for the black nodes
                break;
            case TreeType.AVL:
            default:
                cr.SetSourceRGB(0, 0, 0); // Black color for AVL Tree nodes
                break;
        }

        cr.Arc(x, y, radius, 0, 2 * Math.PI);
        cr.Fill(); // Fill the circle with the current color

        cr.SelectFontFace("Arial", FontSlant.Normal, FontWeight.Bold);
        cr.SetFontSize(13);
        TextExtents te = cr.TextExtents(text);

        // Draw the text outline
        cr.SetSourceRGB(0, 0, 0); // Set color to black
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx != 0 || dy != 0)
                {
                    cr.MoveTo(x - te.Width / 2 + dx, y + te.Height / 2 + dy);
                    cr.ShowText(text);
                }
            }
        }

        // Draw the actual text
        cr.SetSourceRGB(1, 1, 1); // Set color to white
        cr.MoveTo(x - te.Width / 2, y + te.Height / 2);
        cr.ShowText(text);

        if (currentTreeType == TreeType.AVL)
        {
            string balanceText = $"{balanceFactor}";
            TextExtents bfTe = cr.TextExtents(balanceText);
            double offsetX = 3; // Adjust this value to move the text more or less to the right
            double offsetY = -10; // Adjust this value to move the text more or less down

            // Draw the black outline
            cr.SetSourceRGB(0, 0, 0); // Set color to black
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    cr.MoveTo(x + radius + offsetX + i - bfTe.Width / 2, y + radius + offsetY + bfTe.Height + j);
                    cr.ShowText(balanceText);
                }
            }

            // Draw the green text
            cr.SetSourceRGB(0, 1, 0); // Set color to green
            cr.MoveTo(x + radius + offsetX - bfTe.Width / 2, y + radius + offsetY + bfTe.Height);
            cr.ShowText(balanceText);
        }
    }
    public static void Main()
    {
        Application.Init();
        new TreeVisualizer();
        Application.Run();
    }
}