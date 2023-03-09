namespace Mantis.DocumentEngine;

public enum GridLineType{Large,Medium,Small}

public struct GridLineInfo
{
    public GridLineType Type;
    public double Offset;
}
public class GridPlotter : Plotter
{
    public Transform GridTransform;
    
    public GridPlotter(GraphCreator parent) : base(parent)
    {
        GridTransform = new Transform(GraphRoot);
    }

    public override void Plot()
    {
        // Plot X
        foreach (GridLineInfo info in LayoutManager.XAxis.GenerateGrid())
        {
            LineGraphic lineGraphic = new LineGraphic()
            {
                Start = new Vector2(info.Offset, 0),
                End = new Vector2(info.Offset, LayoutManager.YAxis.Length),
                Style = GetStyle(info)
            };
            GridTransform.Add(lineGraphic);
        }
        
        //Plot Y
        foreach (GridLineInfo info in LayoutManager.YAxis.GenerateGrid())
        {
            LineGraphic lineGraphic = new LineGraphic()
            {
                Start = new Vector2(0,info.Offset),
                End = new Vector2( LayoutManager.XAxis.Length,info.Offset),
                Style = GetStyle(info)
            };
            GridTransform.Add(lineGraphic);
        }
        
    }

    private LineStyle GetStyle(GridLineInfo info)
    {
        switch (info.Type) 
        {
            case GridLineType.Large: return LayoutManager.SketchBookStyle.GridLineLarge;
            case GridLineType.Medium: return LayoutManager.SketchBookStyle.GridLineMedium;
            case GridLineType.Small: return LayoutManager.SketchBookStyle.GridLineSmall;
        }

        return LayoutManager.SketchBookStyle.GridLineSmall;
    }
}