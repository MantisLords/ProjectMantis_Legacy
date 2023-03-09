namespace Mantis.DocumentEngine;

public class TextGraphic : Graphic
{
    public TextStyle Style;
    public Vector2 Position;
    public string Text;
    public override void Draw(Canvas canvas)
    {
        canvas.DrawText(MP(Position),Style,Text);
    }
}