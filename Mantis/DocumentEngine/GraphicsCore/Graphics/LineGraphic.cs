namespace Mantis.DocumentEngine;

public class LineGraphic : Graphic
{
    public Vector2 Start;
    public Vector2 End;

    public LineStyle Style = new LineStyle();
    
    
    public override void Draw(Canvas canvas)
    {
        canvas.DrawLine(GlobalPosition.MultiplyPoint(Start),GlobalPosition.MultiplyPoint(End),Style);
    }
}