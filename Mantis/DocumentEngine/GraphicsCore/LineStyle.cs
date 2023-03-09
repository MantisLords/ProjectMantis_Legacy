using PdfSharp.Drawing;

namespace Mantis.DocumentEngine;

public class LineStyle
{
    public XPen Pen;

    public LineStyle()
    {
       Pen = new XPen(XColors.Black,2);
    }

    public LineStyle(XColor color, double width)
    {
        Pen = new XPen(color, width);
    }

    public void SetWidth(double width)
    {
        Pen.Width = width;
    }

    public static implicit operator XPen(LineStyle style)
    {
        return style.Pen;
    }
}