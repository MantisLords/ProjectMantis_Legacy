namespace Mantis.DocumentEngine;

public class DataMarkErrorGraphic : Graphic
{
    public Vector2 Position;

    public double Up;
    public double Down;
    public double Right;
    public double Left;
    public const double MinLength = 1;
    public const double EndLength = 1;
    public LineStyle Style;
    
    public override void Draw(Canvas canvas)
    {
        if (Up > MinLength && Down > MinLength)
        {
            Vector2 upPos = new Vector2(Position.x,Up);
            Vector2 downPos = new Vector2(Position.x,Down);
            
            canvas.DrawLine(MP(upPos),MP(downPos),Style);
            canvas.DrawLine(MP(upPos + Vector2.left * EndLength),MP(upPos + Vector2.right * EndLength),Style);
            canvas.DrawLine(MP(downPos + Vector2.left * EndLength),MP(downPos + Vector2.right * EndLength),Style);
        }
        
        if (Right > MinLength && Left > MinLength)
        {
            Vector2 leftPos = new Vector2(Left,Position.y);
            Vector2 rightPos = new Vector2(Right,Position.y);
            
            canvas.DrawLine(MP(leftPos),MP(rightPos),Style);
            canvas.DrawLine(MP(leftPos + Vector2.up * EndLength),MP(leftPos + Vector2.down * EndLength),Style);
            canvas.DrawLine(MP(rightPos + Vector2.up * EndLength),MP(rightPos + Vector2.down * EndLength),Style);
        }
    }
}