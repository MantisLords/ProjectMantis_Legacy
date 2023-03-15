using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public static class Part_3_ResonanceCurves
{
    public static ErDouble T0 = new(1.902);
    public static ErDouble W0 = 2 * Math.PI / T0;
    public static ErDouble A0 = new(0.054, 0.001);
    public static ErDouble delta4 = new ErDouble(0.157, 0.016);
    public static ErDouble deltaQuotient = delta4 / W0;
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator currentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;

    public static void Generate()
    {
        double[,] daten200mA =
        {
            { 102, -0.108, -0.292 },
            { 149, -0.071, -0.331 },
            { 180, 0.060, -0.463 },
            { 193, 0.355, -0.759 },
            { 202, 1.050, -1.463 },
            { 206, 1.036, -1.405 },
            { 212, 1.114, -1.529 },
            { 219, 0.437, -0.826 },
            { 238, -0.023, -0.391 },
            { 285, -0.105, -0.310 }
        }; // frequenz (Hz), A+ (V) ,A- (V)

        double[,] daten400mA =
        {
            {46,-0.144,-0.267},
            {98,-0.117,-0.283},
            {159,-0.073,-0.331},
            {187,0.061,-0.458},
            {201,0.187,-0.594},
            {210,0.262,-0.652},
            {234,0.012,-0.437},
            {274,-0.108,-0.292}
            
        }; //frequenz (Hz), A+ (V), A- (V)

        var resDaten200mA = InitializeData(daten200mA);

        var resDaten400mA = InitializeData(daten400mA);
        SketchBook sketchBook = new SketchBook("Resonanzkurve bei 200mA");
        PleaseEndThisAgony(resDaten200mA,"200", sketchBook);
        List<WheelData> data = InitializeDataH();
        SketchBook sketchBook2 = new SketchBook("Resonanzkurve bei 400mA");
        var points = data.Select(e => new DataPoint(e.freqQuotient, e.AmplitudeQuotient)).ToList();
        sketchBook2.Add(new DataSetSketch("bei 400mA",points));
        PleaseEndThisAgony(resDaten400mA,"400",sketchBook2);

    }

    public static void PleaseEndThisAgony(List<ResDaten> daten, string strom,SketchBook sketchBook)
    {
        for (var i = 0; i < daten.Count; i++)
        {
            var e = daten[i];


            e.W = 2 * Math.PI * e.Frequenz / 400;
            e.A = (e.Aplus - e.Aminus) / 2;
            e.WQuotient = e.W / W0;
            e.AQuotient = e.A / A0;
            daten[i] = e;
        }

        currentTableCreator.AddTable($"Resonanz bei {strom}mA - Teil 1",
            new[]
            {
                "Frequenz", "w", "w/w0", "A+"
            },
            daten.Select(e => new[]
            {
                e.Frequenz.ToString(), e.W.ToString(), e.WQuotient.ToString(), e.Aplus.ToString()
            }),
            GlobalStyles.StandardTable);

        //currentTableCreator.AddPageBreak();
        currentTableCreator.AddTable($"Resonanz bei {strom}mA - Teil 2",
    new string[]{"A-", "A", "A/A0"},
    daten.Select(e => new string[] {e.Aminus.ToString(), e.A.ToString(), e.AQuotient.ToString()}),
    GlobalStyles.StandardTable);
        List<DataPoint> points = daten.Select(e => new DataPoint(e.WQuotient, e.AQuotient)).ToList();
    sketchBook.Add(new DataSetSketch($"Resonanzkurve bei {strom}mA", points));

    GraphCreator creator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("w/w0"),
        LinearAxis.Auto("A/A0"),
        GraphOrientation.Landscape);
    }

    public static List<WheelData> InitializeDataH()
    {
        List<WheelData> data = new List<WheelData>();
        double sum = 0;
        for (int i = 0; i < 30; i++)
        {
            data.Add(new WheelData()
                {
                    AmplitudeQuotient = 1/(Math.Sqrt(Math.Pow(1-Math.Pow(sum,2),2)+Math.Pow(2*sum*deltaQuotient.Value,2))),
                    freqQuotient = sum
                }
            );
            if (sum >= 0.8 && sum < 1.1)
            {
                sum += 0.02;
            }
            else
            {
                sum += 0.1; 
            }
         
        }

        return data;
    }
    public static List<ResDaten> InitializeData(double[,] rawData)
    {
        var data = new List<ResDaten>();
        for (var i = 0; i < rawData.GetLength(0); i++)
            data.Add(new ResDaten
                {
                    Frequenz = new ErDouble(rawData[i, 0], Main_Trial_25_PohlWheel.FREQUENCY_ERROR),
                    Aplus = new ErDouble(rawData[i, 1], Main_Trial_25_PohlWheel.VOLTAGE_ERROR),
                    Aminus = new ErDouble(rawData[i, 2], Main_Trial_25_PohlWheel.VOLTAGE_ERROR)
                }
            );

        return data;
    }

    public struct ResDaten
    {
        public ErDouble Frequenz;
        public ErDouble W;
        public ErDouble WQuotient;
        public ErDouble Aplus;
        public ErDouble Aminus;
        public ErDouble A;
        public ErDouble AQuotient;
    }
    public struct WheelData
    {
        public double freqQuotient;
        public double AmplitudeQuotient;
    }
}