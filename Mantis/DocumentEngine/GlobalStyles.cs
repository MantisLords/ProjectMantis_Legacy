using Mantis.DocumentEngine.TableCreator;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;

namespace Mantis.DocumentEngine;

public static class GlobalStyles
{
    public static readonly SketchBookStyle StandardGraphStyle = new SketchBookStyle()
    {
        //GridStyles
        GridLineLarge = new LineStyle(XColors.LightGreen, 1),
        GridLineMedium = new LineStyle(XColors.PaleGreen, 0.5),
        GridLineSmall = new LineStyle(XColors.PaleGreen, 0.25),

        //Labeling
        LabelArrow = new LineStyle(XColors.DarkSlateGray, 0.75),
        LabelLine = new LineStyle(XColors.DarkSlateGray, 0.5),
        LabelText = new TextStyle(size: 8, font: "Times New Roman"),

        //DataMarks
        DataMark = new LineStyle(XColors.Black, 0.5),
        DataMarkSize = 1,
        DataMarkErrorBounds = new LineStyle(XColors.Black, 0.5),
        
        //Title
        GraphTitle = new TextStyle(size: 10, font: "Times New Roman"),
        
        //Curve
        Straight = new LineStyle(XColors.Black,0.5)

    };

    public static readonly TableStyle StandardTable = new TableStyle()
        {ColumnWidth = Unit.FromCentimeter(5), RowHeight = Unit.FromCentimeter(0.75),BorderWidth = 0.75};
}