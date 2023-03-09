namespace Mantis.DocumentEngine;

public abstract class SketchCommand
{
    public abstract void Plot(LayoutManager layoutManager, Transform sketchRoot,SketchBookStyle style);
}