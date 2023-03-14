using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public class FormattedPageCreator : PageCreator
{
    public Document MigraDoc { get; }
    
    public FormattedPageCreator(MantisDocument parent) : base(parent)
    {
        MigraDoc = new Document();
    }

    public override void RenderNewPage(PdfDocument document)
    {
        // Create a renderer and prepare (=layout) the document
        DocumentRenderer docRenderer = new DocumentRenderer(MigraDoc);
        docRenderer.PrepareDocument();

        int pageCount = docRenderer.FormattedDocument.PageCount;

        for (int i = 0; i < pageCount; i++)
        {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            // HACK²
            gfx.MUH = PdfFontEncoding.Unicode;
            //gfx.MFEH = PdfFontEmbedding.Default;
            
            docRenderer.RenderPage(gfx,i+1);
        }
    }
    
    public void Print(string text)
    {
        MigraDoc.LastSection.AddParagraph(text);
    }

    public void AddPageBreak()
    {
        MigraDoc.LastSection.AddPageBreak();
    }

    public void AddNewSection()
    {
        MigraDoc.AddSection();
    }
}