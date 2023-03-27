using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Microsoft.VisualBasic;
using MigraDoc.DocumentObjectModel;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public static class Part_3_ResonanceCurves
{
    public static ErDouble T0 = new(1.877,0.003261);
    public static ErDouble W0 = 2 * Math.PI / T0;
    public static ErDouble A0 = new ErDouble(0.0638,0.00131);
    public static ErDouble delta4 = new ErDouble(0.1560, 0.0078);
    public static ErDouble deltaQuotient = delta4 / W0;
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator currentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;

    public static void Generate()
    {
        double[,] daten200mA =
        {
            {43,0.0725,1.45,1.84+Math.PI},
            {86,0.0999,-3.40+Math.PI,3.29},
            {129,0.117,4.86,2.15},
            {174,0.212,-1.97+Math.PI,1.57+Math.PI},
            {193,0.418,4.54,1.84},
            {198,0.569,0.833,4.41},
            {202,0.784,2.39,-3.37+Math.PI},
            {206,1.07,8.15+Math.PI,5.65+Math.PI},
            {211,2.22,1.02,2.02+Math.PI},
            {215,2.62,0.0795,1.99+Math.PI},
            {219,0.932,0.379,-3.35+Math.PI},
            {223,0.594,4.37,3.85},
            {227,0.439,2.65,2.17},
            {232,0.342,4.23,3.81},
            {236,0.289,2.88,2.47},
            {258,0.154,1.22,0.882},
            {301,0.0759,1.03,0.749},
            {344,0.0507,7.68+Math.PI,1.16+Math.PI}
        }; // frequenz (Hz), A in V , Phi1, Phi2

        double[,] daten400mA =
        {
            {44,0.0719,4.26,1.52},
            {86,0.100,4.41,1.72},
            {133,0.118,2.93,-2.88+Math.PI},
            {173,0.198,2.91,-2.83+Math.PI},
            {194,0.356,0.290,0.987+Math.PI},
            {198,0.432,0.597,-1.75},
            {202,0.523,1.25,-0.992},
            {206,0.634,0.174,1.290+Math.PI},
            {211,0.725,4.66,3.02},
            {215,0.667,10.1+Math.PI,-0.615},
            {219,0.540,8.54+Math.PI,4.35},
            {223,0.446,-1.01,-1.24+Math.PI+Math.PI},
            {227,0.360,4.28,3.50},
            {231,0.301,1.72,1.04},
            {236,0.256,2.24,1.61},
            {258,0.146,2.89,2.43},
            {302,0.0734,2.39,2.06},
            {344,0.0507,-1.50,-4.90+Math.PI}
            
        }; //frequenz (Hz), A+ (V), A- (V)

        var resDaten200mA = InitializeData(daten200mA);

        var resDaten400mA = InitializeData(daten400mA);
        
        PleaseEndThisAgony(resDaten200mA,"200", new ErDouble(0.0314,0.0031));

        PleaseEndThisAgony(resDaten400mA,"400",new ErDouble(0.1560,0.0078));

    }

    public static void PleaseEndThisAgony(List<ResDaten> daten, string strom,ErDouble dampingCoefficient)
    {
        for (var i = 0; i < daten.Count; i++)
        {
            var e = daten[i];
            
            e.W = 2 * Math.PI * e.Frequenz / 400;
            e.WQuotient = e.W / W0;
            e.AQuotient = e.A / A0;
            daten[i] = e;
        }

        TableStyle smallTable = GlobalStyles.StandardTable;
        smallTable.ColumnWidth = Unit.FromCentimeter(4);

        currentTableCreator.AddTable($"Resonanz bei {strom}mA - Teil 1",
            new[]
            {
                "Frequenz / Hz", "w / s^-1", "w/w0",
            },
            daten.Select(e => new[]
            {
                e.Frequenz.ToString(), e.W.ToString(), e.WQuotient.ToString(),
            }),
            smallTable);

        currentTableCreator.AddPageBreak();

        currentTableCreator.AddTable($"Resonanz bei {strom}mA - Teil 2",
    new string[]{"Phasenvershiebung" ,"A / mA", "A/A0"},
    daten.Select(e => new string[] {e.PhaseDifference.ToString(),e.A.ToString(), e.AQuotient.ToString()}),
    smallTable);
        currentTableCreator.AddPageBreak();
        
        
        List<DataPoint> points = daten.Select(e => new DataPoint(e.WQuotient, e.AQuotient)).ToList();
        SketchBook sketchBook = new SketchBook($"Resonanzkurve bei {strom}mA");
        sketchBook.Add(new DataSetSketch($"Resonanzkurve bei {strom}mA", points));
    
    List<WheelData> theoreticalValues = Homework.CalculateTheoreticalResonanzCurve((dampingCoefficient/W0).Value,0.005);
    
    var theroeticalPoints = theoreticalValues.Select(e => new DataPoint(e.freqQuotient, e.AmplitudeQuotient)).ToList();
    sketchBook.Add(new DataSetSketch("theo",theroeticalPoints){Type = DataMarkType.Circle,Size = 0.5});
    
    currentTableCreator.AddTable($"Theoretische Werte für I = {strom}mA",
        headers:new string[]{"w/w0","A/A0"},
        content:theoreticalValues.Select(e => new string[]{e.freqQuotient.ToString("G4"),e.AmplitudeQuotient.ToString("G4")}));

    GraphCreator creator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("w/w0"),
        LinearAxis.Auto("A/A0"),
        GraphOrientation.Landscape);
    
    SketchBook meinSketchBook = new SketchBook("Phasenverschiebung");
    List<DataPoint> phasendiffPunkte = daten.Select(e => new DataPoint(e.WQuotient, e.PhaseDifference)).ToList();
    meinSketchBook.Add(new DataSetSketch("Phasenverschiebung", phasendiffPunkte));
    GraphCreator meinCreator = new GraphCreator(CurrentDocument, meinSketchBook, LinearAxis.Auto("w/w0"),
        LinearAxis.Auto("Phi / rad"), GraphOrientation.Landscape);
    
    //max
    ErDouble maxAQutient = Max(daten, e => e.AQuotient.Value).AQuotient;
    ErDouble deltaCalc = W0 / maxAQutient / 2;
    currentTableCreator.Print($"Max A/A0: {maxAQutient} Resulting Dampening {deltaCalc}");

    }

    private static T Max<T>(List<T> source,Func<T,double> selector)
    {
        T maxEl = default(T);
        double maxValue = Double.NegativeInfinity;

        foreach (T e in source)
        {
            if (selector.Invoke(e) > maxValue)
            {
                maxValue = selector(e);
                maxEl = e;
            }
        }

        return maxEl;
    }
    
    public static List<ResDaten> InitializeData(double[,] rawData)
    {
        var data = new List<ResDaten>();
        for (var i = 0; i < rawData.GetLength(0); i++)
        {
            var phaseDif = PhaseDif(rawData[i,2],rawData[i,3]);
            data.Add(new ResDaten
                {
                    Frequenz = new ErDouble(rawData[i, 0], Main_Trial_25_PohlWheel.FREQUENCY_ERROR),
                    A = new ErDouble(rawData[i, 1], rawData[i, 1] * 0.05 + 0.001),
                    PhaseDifference = phaseDif

                }
            );
        }

        return data;
    }

    private static ErDouble PhaseDif(double phi1,double phi2)
    {
        phi1 = Mod2PI(phi1);
        phi2 = Mod2PI(phi2);

        if (phi1 > phi2)
            phi2 += 2 * Math.PI;

        phi2 -= Math.PI;
        
        ErDouble phaseDifRad = new ErDouble(phi2, phi2 * Main_Trial_25_PohlWheel.PHASE_DIFF_REL_ERROR) -
                               new ErDouble(phi1, phi1 * Main_Trial_25_PohlWheel.PHASE_DIFF_REL_ERROR);

        phaseDifRad -= 0.39;
        return phaseDifRad / Math.PI;
    }

    private static double Mod2PI(double value)
    {
        return value - Math.Floor((value + 20 * Math.PI) / 2 / Math.PI) * 2 * Math.PI;
    }

    public struct ResDaten
    {
        public ErDouble Frequenz;
        public ErDouble W;
        public ErDouble WQuotient;
        public ErDouble A;
        public ErDouble AQuotient;
        public ErDouble PhaseDifference;
    }
}

public struct WheelData
{
    public double freqQuotient;
    public double AmplitudeQuotient;
}