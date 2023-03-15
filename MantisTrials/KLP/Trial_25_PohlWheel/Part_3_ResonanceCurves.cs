using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public static class Part_3_ResonanceCurves
{
    public static ErDouble T0 = new(1.902);
    public static ErDouble W0 = 2 * Math.PI / T0;
    public static ErDouble A0 = new(0.054, 0.001);
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
            {159,-0.073,-0.331}
        }; //frequenz (Hz), A+ (V), A- (V)

        var resDaten200mA = InitializeData(daten200mA);

        var resDaten400mA = InitializeData(daten400mA);
        PleaseEndThisAgony(resDaten200mA,"200");
        PleaseEndThisAgony(resDaten400mA, "400");


        // for (var i = 0; i < resDaten200mA.Count; i++)
        // {
        //     var e = resDaten200mA[i];
        //
        //
        //     e.W = 2 * Math.PI * e.Frequenz / 400;
        //     e.A = (e.Aplus - e.Aminus) / 2;
        //     e.WQuotient = e.W / W0;
        //     e.AQuotient = e.A / A0;
        //     resDaten200mA[i] = e;
        // }
        //
        // currentTableCreator.AddTable("Resonanz bei 200mA - Teil 1",
        //     new[]
        //     {
        //         "Frequenz", "w", "w/w0", "A+"
        //     },
        //     resDaten200mA.Select(e => new[]
        //     {
        //         e.Frequenz.ToString(), e.W.ToString(), e.WQuotient.ToString(), e.Aplus.ToString()
        //     }),
        //     GlobalStyles.StandardTable);
        //
        // //currentTableCreator.AddPageBreak();
        // currentTableCreator.AddTable("Resonanz bei 200mA - Teil 2",
        //     new[]
        //     {
        //         "A-", "A", "A/A0"
        //     },
        //     resDaten200mA.Select(e => new[]
        //     {
        //         e.Aminus.ToString(), e.A.ToString(), e.AQuotient.ToString()
        //     }),
        //     GlobalStyles.StandardTable);
        //
        // var sketchBook = new SketchBook("Resonanzkurve bai 200mA");
        // var points = resDaten200mA.Select(e => new DataPoint(e.WQuotient, e.AQuotient)).ToList();
        // sketchBook.Add(new DataSetSketch("Resonanzkurve bei 200mA", points));
        //
        // var creator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("w/w0"),
        //     LinearAxis.Auto("A/A0"),
        //     GraphOrientation.Landscape);
    }

    public static void PleaseEndThisAgony(List<ResDaten> daten, string strom)
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

        SketchBook sketchBook = new SketchBook($"Resonanzkurve bei {strom}mA");
    List<DataPoint> points = daten.Select(e => new DataPoint(e.WQuotient, e.AQuotient)).ToList();
    sketchBook.Add(new DataSetSketch($"Resonanzkurve bei {strom}mA", points));

    GraphCreator creator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("w/w0"),
        LinearAxis.Auto("A/A0"),
        GraphOrientation.Landscape);
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
}