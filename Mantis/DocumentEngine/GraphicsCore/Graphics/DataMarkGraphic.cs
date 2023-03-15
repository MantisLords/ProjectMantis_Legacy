namespace Mantis.DocumentEngine;

public enum DataMarkType{Cross = 0,CrossCircle = 1,CrossRectangle = 2,Circle = 3,Rectangle = 4}

public class DataMarkGraphic : Graphic
{
    private static readonly double SQRT2 = Math.Sqrt(2);

    public DataMarkType Type = DataMarkType.Cross;
    public double Size = 1;
    public LineStyle Style;
    
    public override void Draw(Canvas canvas)
    {
        switch (Type)
        {
            case DataMarkType.Cross:
                DrawCross(canvas);
                break;
            case DataMarkType.CrossCircle:
                DrawCross(canvas);
                DrawCircle(canvas);
                break;
            case DataMarkType.CrossRectangle:
                DrawCross(canvas);
                DrawSquare(canvas);
                break;
            case DataMarkType.Circle:
                DrawCircle(canvas);
                break;
            case DataMarkType.Rectangle:
                DrawSquare(canvas);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DrawCross(Canvas canvas)
    {
        Vector2 ur = new Vector2(Size, Size) / 2;
        Vector2 ul = new Vector2(-Size, Size) / 2;

        canvas.DrawLine(MP(ur), MP(-ur), Style);
        canvas.DrawLine(MP(ul), MP(-ul), Style);
    }

    private void DrawCircle(Canvas canvas)
    {
        canvas.DrawCircle(MP(Vector2.zero),Size*SQRT2 / 2,Style);
    }

    private void DrawSquare(Canvas canvas)
    {
        canvas.DrawSquare(MP(Vector2.zero),Size,Style);
    }
}