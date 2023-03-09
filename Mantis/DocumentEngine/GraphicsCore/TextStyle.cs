using PdfSharp.Drawing;

namespace Mantis.DocumentEngine;

public class TextStyle
{
    public XBrush Brush;
    public XFont Font;

    public TextStyle()
    {
        Brush = XBrushes.DarkSlateGray;
        Font = new XFont("Times New Roman", 20);
    }

    public TextStyle(XBrush brush, XFont font)
    {
        Brush = brush;
        Font = font;
    }

    public TextStyle(int size = 20,string font = "Times New Roman")
    {
        Brush = XBrushes.DarkSlateGray;
        Font = new XFont(font, size);
    }
}