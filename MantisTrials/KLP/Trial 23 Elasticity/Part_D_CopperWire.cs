using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;

namespace MantisTrials.KLP.Trial_23_Elasticity;

public static class Part_D_CopperWire
{
    private static MantisDocument CurrentDocument => Main_Trail_23_Elasticity.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;
    
    public static void Generate()
    {
        double[,] rawData = new double[,]
        {
            {19.7777 + 20.0664, 523},
            {19.7206, 524},
            {19.7824, 525},
            {19.8774, 526},
            {9.9014, 526},
            {9.9382, 527},
            {9.9463, 530},
            {9.9449, 533},
            {9.9034, 540},
            {10.1025, 548},
            {9.948, 561},
            {9.9572, 578}
        };

        ErDouble defaultLength = new ErDouble(522,Main_Trail_23_Elasticity.L_ERROR);
        ErDouble wireCrossSection = new ErDouble(0.01, 0.002); // mm^2

        List<WireData> data = InitializeRawData(rawData);

        data = data.Select(e => CalculateEpsilonSigma(e,defaultLength,wireCrossSection)).ToList();
        
        //Graph
        SketchBook sketchBook = new SketchBook("D Copper Wire");

        List<DataPoint> points = data.Select(e => new DataPoint(e.epsilon, e.sigma.Mul10E(-8))).ToList();
        sketchBook.Add(new DataSetSketch(points));

        List<DataPoint> pointsInLinear = (from e in data
                    where e.m.Value < 120
                    select new DataPoint(e.epsilon,e.sigma.Mul10E(-8))).ToList();
        LinearMinMaxFit fit = new LinearMinMaxFit(pointsInLinear);
        fit.SetReading(0.5,true,2.125,true);
        sketchBook.Add(new StraightSketch<LinearFunction>(fit));

        GraphCreator creator = new GraphCreator(document: CurrentDocument,
            sketchBook: sketchBook,
            xAxis: LinearAxis.Auto("Epsilon"),
            yAxis: LinearAxis.Auto("Sigma / N/m^2 * 10^7"),
            orientation: GraphOrientation.Landscape);
        
        CurrentTableCreator.AddTable(tablename:"Tab15: D Messdaten - Dehnung eines Kupferdrahts",
            headers: new string[]{$"{GreekAlphabet.EPSILON}",$"{GreekAlphabet.SIGMA} / N/m^2 * 10^8"},
            content:data.Select(e => new string[]{e.epsilon.ToString(),e.sigma.Mul10E(-8).ToString()}).ToArray(),
            style:GlobalStyles.StandardTable,
            times:2);
        
        CurrentTableCreator.Print(fit.ToString());

        ErDouble elasticity = fit.GetSlope().Mul10E(8);
        CurrentTableCreator.Print($"Elasticity: {elasticity.Mul10E(-5)} MPa");

    }

    private static List<WireData> InitializeRawData(double[,] rawData)
    {
        List<WireData> data = new List<WireData>();
        double mass = 0;
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            mass += rawData[i, 0];
            data.Add(new WireData()
            {
                m = new ErDouble(mass,mass * 0.0001+0.0001),
                l = new ErDouble(rawData[i,1],Main_Trail_23_Elasticity.L_ERROR)
            });
        }

        return data;
    }

    private static WireData CalculateEpsilonSigma(WireData dataPoint,ErDouble l0,ErDouble crossSection)
    {
        dataPoint.epsilon = dataPoint.l / l0 - 1;
        // m in g, cross Section in mm^2
        dataPoint.sigma = dataPoint.m.Mul10E(-3) * Main_Trail_23_Elasticity.G / crossSection.Mul10E(-6);
        return dataPoint;
    }

    private struct WireData
    {
        public ErDouble m;
        public ErDouble l;
        public ErDouble epsilon;
        public ErDouble sigma;
    }
}