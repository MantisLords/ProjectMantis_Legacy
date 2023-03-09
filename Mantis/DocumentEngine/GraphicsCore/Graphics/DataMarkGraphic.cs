namespace Mantis.DocumentEngine;

public class DataMarkGraphic : Graphic
{
    public enum MarkType{Cross,Circle}

    public MarkType Type = MarkType.Cross;
    public double Size = 1;
    public LineStyle Style;
    
    public override void Draw(Canvas canvas)
    {
        if (Type == MarkType.Cross)
        {
            Vector2 ur = new Vector2(Size, Size) / 2;
            Vector2 ul = new Vector2(-Size, Size) / 2;

            canvas.DrawLine(MP(ur), MP(-ur), Style);
            canvas.DrawLine(MP(ul), MP(-ul), Style);
        }else if (Type == MarkType.Circle)
        {
            canvas.DrawCircle(MP(Vector2.zero),Size,Style);
        }
    }
}