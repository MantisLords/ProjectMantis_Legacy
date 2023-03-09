namespace Mantis.DocumentEngine;

public class ArrowGraphic : Graphic
{
    public LineStyle Style;

    public Vector2 Start;
    public Vector2 End;

    public double TopAngle = 60;
    public double TopLength = 2;
    
    public override void Draw(Canvas canvas)
    {
        canvas.DrawLine(MP(Start),MP(End),Style);

        Vector2 direction = (End - Start).normalized;

        Vector2 upTop = new Vector2();
        upTop.x = Math.Cos(TopAngle) * direction.x - Math.Sin(TopAngle) * direction.y;
        upTop.y = Math.Sin(TopAngle) * direction.x + Math.Cos(TopAngle) * direction.y;
        upTop *= TopLength;
        upTop += End;
        
        canvas.DrawLine(MP(End),MP(upTop),Style);
        
        Vector2 downTop = new Vector2();
        downTop.x = Math.Cos(TopAngle) * direction.x + Math.Sin(TopAngle) * direction.y;
        downTop.y = -Math.Sin(TopAngle) * direction.x + Math.Cos(TopAngle) * direction.y;
        downTop *= TopLength;
        downTop += End;
        
        canvas.DrawLine(MP(End),MP(downTop),Style);
        
    }
}