using System.Data;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public class MantisDocument
{
    public static readonly PrinterCorrections PrinterPhysicLibraryUniWue =
        new PrinterCorrections(scale: new Vector2(180.0 / 175.0 * 180.0 / 179.0, 280.0 / 271.0),
            offset: new Vector2(-4, -4));
    
    public PdfDocument PdfDocument { get; }
    public List<PageCreator> Pages { get; }

    public PrinterCorrections Corrections = new PrinterCorrections();

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
        Random rnd = new Random();
        int random = rnd.Next(0, 6);
        switch (random)
        {
            case 0:Console.WriteLine("Nützlicher Tipp: wenn dein Fehler zu klein ist multipliziere ihn mit 10!");
                break;
            case 1:Console.WriteLine("Vergesse nie: Traue keinem Wert den du nicht selbst gefälscht hast!");
                break;
            case 2:Console.WriteLine("Erfinde Messdaten...");
                break;
            case 3 : Console.WriteLine("Nützlicher Tipp: wenn dein Fehler zu klein ist multipliziere ihn mit 10!");
                break;
            case 4: Console.WriteLine("Rufe Standpunktdaten ab für genaue Bestimmung der Erbeschleunigung...");
                Thread.Sleep(1000);
                Console.WriteLine("g = 10");
                break;
            case 5: Console.WriteLine("Kopiere Altprotokolle...");
                break;
            case 6:Console.WriteLine("Wer sich auf Mantis verlässt wird von mentis verlassen!");
                break;
        }
        Console.WriteLine($"{filepath} saved");
    }
}