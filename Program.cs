using Gtk;
using Cairo;
using System;

public class RBTreeVisualizer : Window
{
    private RedBlackTree<int> tree;
    private double next_x = 0;
    private double nodeDistance = 30; // Distance between nodes

public RBTreeVisualizer() : base("Red-Black Tree Visualizer")
{
    tree = new RedBlackTree<int>();

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

    // Create the Delete Node button
    Button deleteNodeButton = new Button("Delete Node");
    deleteNodeButton.Clicked += DeleteNodeButton_Clicked;
    buttonBox.Add(deleteNodeButton);

    // Create the Insert Random Value button
    Button insertRandomButton = new Button("Insert Random Value");
    insertRandomButton.Clicked += InsertRandomButton_Clicked;
    buttonBox.Add(insertRandomButton);

    // Add the VBox to the window
    Add(vbox);

    // Set the size of the window
    SetDefaultSize(1400, 600);
    ShowAll();
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

private void AddNodeButton_Clicked(object sender, EventArgs e)
{
    int? value = ShowInputDialog();
    
    if (value.HasValue)
    {
        tree.Insert(value.Value);
        QueueDraw(); // Redraw the tree
    }
}

private void DeleteNodeButton_Clicked(object sender, EventArgs e)
{
    // Add your logic to delete a node here
}

private void InsertRandomButton_Clicked(object sender, EventArgs e)
{
    Random random = new Random();
    tree.Insert(random.Next(1000));

    // Redraw the tree after inserting the random value
    QueueDraw();
}

    void OnDraw(object o, DrawnArgs args)
    {
        var cr = args.Cr;

        // Set line width
        cr.LineWidth = 2.0;

        // Calculate the width of the tree
        double treeWidth = CalculateTreeWidth(tree.Root);

        // Start drawing from the middle
        next_x = treeWidth / 2;

        // Start drawing from root
        Draw(cr, tree.Root, 3);
    }

    double CalculateTreeWidth(RedBlackTree<int>.Node? node)
    {
        if (node == null)
        {
            return 0;
        }

        return 1 + Math.Max(CalculateTreeWidth(node.Left), CalculateTreeWidth(node.Right));
    }

    double Draw(Context cr, RedBlackTree<int>.Node node, double depth)
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

        DrawCircle(cr, my_x * nodeDistance, depth * nodeDistance, 17, node.Value.ToString(), node.IsRed);

        if (node.Right != null)
        {
            right_x = Draw(cr, node.Right, depth + 1.5);
            DrawLine(cr, my_x * nodeDistance, depth * nodeDistance, right_x * nodeDistance, (depth + 1.5) * nodeDistance, 17);
        }

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

    void DrawCircle(Context cr, double x, double y, double radius, string text, bool isRed = false)
    {
        if (isRed)
        {
            cr.SetSourceRGB(1, 0, 0); // Set color to red
        }
        else
        {
            cr.SetSourceRGB(0, 0, 0); // Set color to black
        }

        cr.Arc(x, y, radius, 0, 2 * Math.PI);
        cr.Fill(); // Fill the circle with the current color

        cr.SetSourceRGB(1, 1, 1); // Set color to white
        cr.SelectFontFace("Arial", FontSlant.Normal, FontWeight.Bold);
        cr.SetFontSize(13);
        TextExtents te = cr.TextExtents(text);
        cr.MoveTo(x - te.Width / 2, y + te.Height / 2);
        cr.ShowText(text);
    }

    public static void Main()
    {
        Application.Init();
        new RBTreeVisualizer();
        Application.Run();
    }
}
