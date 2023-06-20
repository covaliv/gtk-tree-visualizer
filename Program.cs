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

        // Add nodes to the tree
        Random random = new Random();
        for (int i = 0; i < 40; i++)
        {
            tree.Insert(random.Next(100));
        }

        // Create a new DrawingArea to display the tree
        DrawingArea drawingArea = new DrawingArea();

        // Connect the Draw event to the OnDraw method
        drawingArea.Drawn += OnDraw;

        // Add the DrawingArea to the window
        Add(drawingArea);

        // Set the size of the window
        SetDefaultSize(800, 600);
        ShowAll();
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

    double CalculateTreeWidth(RedBlackTree<int>.Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return 1 + Math.Max(CalculateTreeWidth(node.Left), CalculateTreeWidth(node.Right));
    }

double Draw(Context cr, RedBlackTree<int>.Node node, double depth)
{
    double left_x = 0, right_x = 0;

if (node.Left != null)
{
    left_x = Draw(cr, node.Left, depth + 1);
    DrawLine(cr, next_x * nodeDistance, depth * nodeDistance + 0.5, left_x * nodeDistance, (depth + 1) * nodeDistance + 0.5, 17);
}

    double my_x = next_x++;

    DrawCircle(cr, my_x * nodeDistance, depth * nodeDistance + 0.5, 17, node.Value.ToString(), node.IsRed);

if (node.Right != null)
{
    right_x = Draw(cr, node.Right, depth + 1);
    DrawLine(cr, my_x * nodeDistance, depth * nodeDistance + 0.5, right_x * nodeDistance, (depth + 1) * nodeDistance + 0.5, 17);
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

void DrawCircle(Context cr, double x, double y, double radius, string text, bool isRed)
{
    // Console.WriteLine("Drawing node with value {0} at ({1}, {2}).", text, x, y);
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
    cr.SetFontSize(10);
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
