using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public class Canvas
{
    private XGraphics gfx;
    public Vector2 dim;

    public const double MM_TO_POINT = 2.8346456693;
    public const double POINT_TO_MM = 0.3527777778;

    public Canvas(Vector2 dim)
    {
        this.dim = dim;
    }

    public void InitializeForRendering(XGraphics gfx)
    {
        if (this.gfx != null)
            throw new Exception("Canvas was already Initialized For Rendering");

        this.gfx = gfx;
    }

    public void DrawLine(Vector2 start, Vector2 end, LineStyle style)
    {
        gfx.DrawLine(style,CP(start),CP(end));
    }

    public void DrawText(Vector2 center, TextStyle style,string text)
    {
        XStringFormat format = new XStringFormat();
        format.Alignment = XStringAlignment.Center;
        format.LineAlignment = XLineAlignment.Center;
        gfx.DrawString(text, style.Font,style.Brush,CP(center),format);
        
    }

    public void DrawCircle(Vector2 center, double radius, LineStyle style)
    {
        gfx.DrawEllipse(style.Pen,GetRect(center,new Vector2(radius*2,radius*2)));
    }
    
    public void DrawSquare(Vector2 center, double sideLength, LineStyle style)
    {
        gfx.DrawRectangle(style.Pen,GetRect(center,new Vector2(sideLength,sideLength)));
    }

    private XPoint CP(Vector2 v)
    {
        return new XPoint(v.x * MM_TO_POINT, (dim.y - v.y) * MM_TO_POINT);
    }

    private XRect GetRect(Vector2 center, Vector2 dim)
    {
        return new XRect(CP(center - dim / 2), CP(center + dim / 2));
    }
}