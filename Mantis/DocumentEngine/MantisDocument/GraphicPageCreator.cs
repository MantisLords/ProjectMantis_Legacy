using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public abstract class GraphicPageCreator : PageCreator
{
    
    public GraphicAccess GraphicAccess { get; }

    protected Canvas Canvas { get; }


    public GraphicPageCreator(MantisDocument parent) : base(parent)
    {
        this.Canvas = new Canvas( new Vector2(210,297));
        GraphicAccess = new GraphicAccess(parent.Corrections);

    }

    public override void RenderNewPage(PdfDocument document)
    {
        PdfPage nextPage = document.AddPage();
        
        XGraphics graphics = XGraphics.FromPdfPage(nextPage);
        
        Canvas.InitializeForRendering(graphics);
        
        GraphicAccess.Render(Canvas);
    }
}