using System.Text;
using Mantis.DocumentEngine;

namespace Mantis.Examples;

public static class SimpleGraphExample
{
    public static void RunBasicExample()
    {
        Console.WriteLine("Running SimpleGraphExample");
        
        //First initialize a new MantisDocument
        MantisDocument document = new MantisDocument();
        
        // Set PrinterCorrections if your printer scales the pdf wierd. Here as an example for the uni printer
        //document.Corrections = MantisDocument.PrinterPhysicLibraryUniWue;

        //Now just generate a random set of data
        List<DataPoint> data = new List<DataPoint>();
        for (double i = 0; i < 20; i++)
        {
            data.Add(new DataPoint(
                new ErDouble(i * 10,2),
                new ErDouble(Math.Sin(i/3)*10+40,2)));
        }

        // To create a graph you first need to create a SketchBook. Here you will add all
        // your data points, straights, curves etc. 
        // In the constructor give the name of your Graph
        SketchBook sketchBook = new SketchBook("Graph Example");
        
        // Now add the List of DataPoints to the sketch book
        sketchBook.Add(new DataSetSketch(data));

        // Lastly add your sketch to the MantisDocument
        // You can control the layout over the Axis
        GraphCreator creator = new GraphCreator(
            document: document,
            sketchBook: sketchBook,
            xAxis: LinearAxis.Auto("X / Unit"),
            yAxis: LinearAxis.Auto("Y / Unit"),
            orientation: GraphOrientation.Portrait
        );
        
        document.Save("SimpleGraphExample.pdf");
        
        
    }
}