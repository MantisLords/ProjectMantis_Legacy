
using System.Diagnostics.CodeAnalysis;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_23_Elasticity;

public static class SalahudinExample
{
    private static MantisDocument CurrentDocument => Main_Trail_23_Elasticity.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;

    public static void Generate()
    {
        double[,] rawData = new double[,]
        {
            { 1.0, 1.0 },
            { 2.0, 2.0 },
            {3.0, 3.0}
        };

        List<RodData> data = InitializeRawData(rawData);
        SketchBook sketchBook = new SketchBook("S/F Diagramm");
        List<DataPoint> points = new List<DataPoint>();
        foreach (RodData e in data)
        {
            points.Add(new DataPoint(e.s, e.F));
        }

        sketchBook.Add(new DataSetSketch(points));
        GraphCreator creator = new GraphCreator(
            document: CurrentDocument, sketchBook: sketchBook, xAxis: LinearAxis.Auto("Auslenkung"),
            yAxis: LinearAxis.Auto("Kraft"), orientation: GraphOrientation.Portrait);
    }

    private static List<RodData> InitializeRawData(Double[,] rawData)
    {
        List < RodData >  data = new List<RodData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add( new RodData()
                {
                    F = new ErDouble(rawData[i,1]*9.81, rawData[i,1]*0.0001),
                    s = new ErDouble(rawData[i,0],0.05),
                    m = new ErDouble(rawData[i,1], rawData[i,1]*0.001)
                }
            );
        }

        return data;
    }
    private struct RodData
    {
        public ErDouble s;
        public ErDouble m;
        public ErDouble F;
    }

}