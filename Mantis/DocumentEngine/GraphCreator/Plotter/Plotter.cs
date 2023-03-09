namespace Mantis.DocumentEngine;

public abstract class Plotter
{
    public Transform GraphRoot { get; }
    public LayoutManager LayoutManager { get; }
    public GraphCreator Parent { get; }

    public Plotter(GraphCreator parent)
    {
        GraphRoot = parent.GraphRoot;
        LayoutManager = parent.LayoutManager;
    }

    public abstract void Plot();
}