using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public enum GraphOrientation{Portrait,Landscape}

public class GraphCreator : GraphicPageCreator
{
    public LayoutManager LayoutManager { get; }
    public SketchBook SketchBook { get; }
    
    public Transform GraphRoot { get; }

    private GraphOrientation GraphOrientation { get; }

    public GraphCreator(MantisDocument document,
        SketchBook sketchBook,
        AxisKernel xAxis, AxisKernel yAxis,
        GraphOrientation orientation = GraphOrientation.Portrait) : this(document, sketchBook, xAxis, yAxis,
        new Vector2(20, 10.5),
        new Vector2(180,280),
        orientation){}
    
    public GraphCreator(MantisDocument document,
        SketchBook sketchBook,
        AxisKernel xAxis,AxisKernel yAxis,
        Vector2 offset,
        Vector2 size,
        GraphOrientation orientation = GraphOrientation.Portrait) 
        : base(document)
    {
        SketchBook = sketchBook;

        GraphOrientation = orientation;

        if (orientation == GraphOrientation.Landscape)
            Canvas.dim = new Vector2(297, 210); //TODO Change hardcoded A4 format

        //Handle Offset
        GraphRoot = new Transform(GraphicAccess.Root);
        GraphRoot.SetPosition(offset);

        LayoutManager = new LayoutManager(xAxis, yAxis, orientation,sketchBook,size);
    }

    private void Plot()
    {
        new GridPlotter(this).Plot();
        new LabelPlotter(this).Plot();
        SketchBook.Plot(LayoutManager,GraphRoot);
    }

    public override void RenderNewPage(PdfDocument document)
    {
        PdfPage nextPage = document.AddPage();
        if (GraphOrientation == GraphOrientation.Landscape)
            nextPage.Orientation = PageOrientation.Landscape;
        
        XGraphics graphics = XGraphics.FromPdfPage(nextPage);
        Canvas.InitializeForRendering(graphics);

        Plot();

        if (GraphOrientation == GraphOrientation.Landscape)
            nextPage.Rotate = 90;
        
        GraphicAccess.Render(Canvas);
    }
}