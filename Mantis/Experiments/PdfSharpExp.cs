// See https://aka.ms/new-console-template for more information

using System.Text;
using Mantis.DocumentEngine;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Mantis.Experiments;

public class PdfSharpExp
{
    public static void MainExp()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        PdfDocument document = new PdfDocument();
        
        PdfPage page = document.AddPage();

        page.Orientation = PageOrientation.Landscape;
        
        XGraphics gfx = XGraphics.FromPdfPage(page);
        
        XFont font = new XFont("Arial", 20, XFontStyle.Bold);

        gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height),
            XStringFormat.Center);
        

        // double width = page.Width;
        //
        // GraphicAccess access = new GraphicAccess();
        // Vector2 dimmensions = new Vector2(page.Width * Canvas.POINT_TO_MM, page.Height * Canvas.POINT_TO_MM);
        // Transform middle = new Transform();
        // middle.SetPosition(new Vector2(page.Width/2,200));
        // access.Root.Add(middle);
        //
        // LineGraphic line = new LineGraphic();
        // line.Start = Vector2.down * (width/4);
        // line.End = Vector2.left * (page.Width)/4;
        //
        // middle.Add(line);
        //
        // access.Render(new Canvas(gfx,dimmensions));
        
        gfx.DrawLine(XPens.DarkGreen, 45, 0, 250, 100);

        page.Rotate = 90;
        
        string filename = "HelloWorld.pdf";
        document.Save(filename);

        //Process.Start(filename);
        
        Console.WriteLine("Hello, World!");
        Console.WriteLine($"Width: {page.Width} Height: {page.Height}");
    }
}