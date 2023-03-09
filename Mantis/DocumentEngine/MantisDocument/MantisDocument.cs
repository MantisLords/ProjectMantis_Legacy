using System.Data;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public class MantisDocument
{
    public static readonly PrinterCorrections PrinterPhysicLibraryUniWue =
        new PrinterCorrections(scale: new Vector2(180.0 / 175.0 * 180.0 / 179.0, 280.0 / 271.0 * 270.0 / 269.0),
            offset: new Vector2(-4, -4));
    
    public PdfDocument PdfDocument { get; }
    public List<PageCreator> Pages { get; }

    public PrinterCorrections Corrections { get; } = default;

    private bool _isRendered = false;

    public MantisDocument(PrinterCorrections corrections) : this()
    {
        Corrections = corrections;
    }

    public MantisDocument()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        PdfDocument = new PdfDocument();
        Pages = new List<PageCreator>();
    }

    internal PdfDocument AddPageInternal(PageCreator child)
    {
        if (_isRendered)
            throw new ArgumentException("Document was already rendered! You must not add more pages.");
        
        Pages.Add(child);

        return PdfDocument;
    }

    public void Render()
    {
        if (_isRendered)
            return;
        
        foreach (var page in Pages)
        {
            page.RenderNewPage(PdfDocument);
        }

        _isRendered = true;
    }

    public void Save(string filepath)
    {
        Render();
        PdfDocument.Save(filepath);
        
        Console.WriteLine($"{filepath} saved");
        Console.WriteLine("<3 Ronny");
    }
}