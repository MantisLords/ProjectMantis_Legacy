using System.Text;
using Mantis.DocumentEngine;

namespace Mantis.Experiments;

public static class SimpleGraphExp
{
    public static void TestGrid()
    {
        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        MantisDocument document = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);

        List<DataPoint> data = new List<DataPoint>();

        for (double i = 0; i < 20; i++)
        {
            data.Add(new DataPoint(
                new ErDouble(i * 10,2),
                new ErDouble(Math.Sin(i/3)*10+40,2)));
        }

        SketchBook emptySketchBook = new SketchBook("GraphExp");
        
        emptySketchBook.Add(new DataSetSketch("",data));

        GraphCreator creator = new GraphCreator(
            document: document,
            sketchBook: emptySketchBook,
            xAxis: LinearAxis.Auto("X / Unit"),
            yAxis: LinearAxis.Auto("Y / Unit"),
            orientation: GraphOrientation.Portrait
        );
        // GraphCreator scaleCreator = new GraphCreator(
        //     document: document,
        //     sketchBook: emptySketchBook,
        //     xAxis: LinearAxis.Auto("X / Unit"),
        //     yAxis: LinearAxis.Auto("Y / Unit"),
        //     orientation: GraphOrientation.Portrait
        // );
        
        // GraphCreator second = new GraphCreator(
        //     document: document,
        //     sketchBook: emptySketchBook,
        //     xAxis: LogAxis.Decade("X / Unit",1,0), 
        //     yAxis: LogAxis.Decade("Y / Unit",1,0),
        //     orientation: GraphOrientation.Landscape
        // );
        
        document.Save("SimpleGraphExp.pdf");
        
        
    }
}