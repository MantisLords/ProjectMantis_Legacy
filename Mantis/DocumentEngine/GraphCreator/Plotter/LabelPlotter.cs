namespace Mantis.DocumentEngine;


public struct LabelPlotInfo
{
    public double Offset;
    public string Label;
}

public class LabelPlotter : Plotter
{
    private Transform xLabel;
    private Transform yLabel;

    private const double LineLength = 3;

    public LabelPlotter(GraphCreator parent) : base(parent)
    {
        xLabel = new Transform(GraphRoot);
        yLabel = new Transform(GraphRoot);
    }

    public override void Plot()
    {
        // Plot X
        xLabel.Add(new ArrowGraphic()
        {
            Start = Vector2.zero,
            End = Vector2.right * LayoutManager.XAxis.Length,
            Style = LayoutManager.SketchBookStyle.LabelArrow,
        });
        xLabel.Add(new TextGraphic()
        {
            Position = new Vector2(LayoutManager.XAxis.Length ,-3),
            Text = LayoutManager.XAxis.AxisName,
            Style = LayoutManager.SketchBookStyle.LabelText
        });
        foreach (LabelPlotInfo info in LayoutManager.XAxis.GenerateLabeling())
        {
            Vector2 pos = Vector2.right * info.Offset;
            xLabel.Add(new LineGraphic()
            {
                Start = pos + Vector2.up * LineLength/2,
                End = pos + Vector2.down * LineLength/2,
                Style = LayoutManager.SketchBookStyle.LabelLine
            });
            
            xLabel.Add(new TextGraphic()
            {
                Position = pos + Vector2.down * 5,
                Style = LayoutManager.SketchBookStyle.LabelText,
                Text = info.Label
            });
        }
        
        // Plot y
        yLabel.Add(new ArrowGraphic()
        {
            Start = Vector2.zero,
            End = Vector2.up * LayoutManager.YAxis.Length,
            Style = LayoutManager.SketchBookStyle.LabelArrow,
        });
        yLabel.Add(new TextGraphic()
        {
            Position = new Vector2(-2,LayoutManager.YAxis.Length + 3),
            Text = LayoutManager.YAxis.AxisName,
            Style = LayoutManager.SketchBookStyle.LabelText
        });
        foreach (LabelPlotInfo info in LayoutManager.YAxis.GenerateLabeling())
        {
            Vector2 pos = Vector2.up * info.Offset;
            yLabel.Add(new LineGraphic()
            {
                Start = pos + Vector2.left * LineLength/2,
                End = pos + Vector2.right * LineLength/2,
                Style = LayoutManager.SketchBookStyle.LabelLine
            });
            
            yLabel.Add(new TextGraphic()
            {
                Position = pos + Vector2.left * 5,
                Style = LayoutManager.SketchBookStyle.LabelText,
                Text = info.Label
            });
        }
    }
}