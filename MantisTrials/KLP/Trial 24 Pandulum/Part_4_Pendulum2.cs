using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_4_Pendulum2
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void Generate()
    {
        double[,] rawData =
        {
            {5, 1.699},
            {5, 1.700},
            {10, 1.701},
            {10, 1.703},
            {15, 1.706},
            {15, 1.706},
            {20, 1.711},
            {20, 1.711},
            {25, 1.719},
            {25, 1.719},
            {30, 1.728},
            {30, 1.729},
            {35, 1.739},
            {35, 1.739},
            {40, 1.752},
            {40, 1.752},
            {45, 1.767},
            {45, 1.767},
            {50, 1.783},
            {50, 1.783},
            {55, 1.800},
            {55, 1.800},
            {60, 1.822},
            {60, 1.822}
        };

        var messungDaten = InitiateDataWithError(rawData);
        var theoretischeWerte = Part_4_Pendulum3.GenerateTheoreticalTQuotient(60);

        messungDaten = messungDaten
            .Select(e => CalculateQuotient(e, new ErDouble(1.698, Main_Trial_24_Pandulum.LASER_ERROR))).ToList();

        var sketchBook = new SketchBook("Pendulum 2");
        var points = new List<DataPoint>();
        foreach (var e in messungDaten) points.Add(new DataPoint(e.angle, e.quotient));
        sketchBook.Add(new DataSetSketch(points));
        var theoPoints = new List<DataPoint>();
        foreach (var e in theoretischeWerte) theoPoints.Add(new DataPoint(e.angleTheo, e.quotientTheo));
        sketchBook.Add(new DataSetSketch(theoPoints));

        var creator = new GraphCreator(CurrentDocument,
            sketchBook,
            LinearAxis.Auto("Phi"),
            LinearAxis.Auto("quotient"));

        var Tabelle = new string[messungDaten.Count][];
        for (var i = 0; i < messungDaten.Count; i++)
        {
            var e = messungDaten[i];
            Tabelle[i] = new[] {e.angle.ToString(), e.quotient.ToString()};
        }

        CurrentTableCreator.AddTable("Messdaten Pendel 2",
            new[] {"Phi", "Quotient"},
            Tabelle, //data.Select(e => new string[]{e.epsilon.ToString(),e.sigma.Mul10E(-8).ToString()}).ToArray(),
            GlobalStyles.StandardTable,
            1);
    }

    private static List<Pendulumdata> InitiateDataWithError(double[,] rawData)
    {
        var data = new List<Pendulumdata>();
        for (var i = 0; i < rawData.GetLength(0); i += 2)
            if (i < rawData.GetLength(0) - 1)
                data.Add(new Pendulumdata
                {
                    angle = new ErDouble(rawData[i, 0], Main_Trial_24_Pandulum.ANGLE_READING_ERROR),

                    period = new ErDouble((rawData[i, 1] + rawData[i + 1, 1]) / 2, 7.1 * Math.Pow(10, -4))
                });

        return data;
    }

    private static Pendulumdata CalculateQuotient(Pendulumdata listdata, ErDouble T0)
    {
        listdata.quotient = (listdata.period - T0) / T0;
        return listdata;
    }

    private struct Pendulumdata
    {
        public ErDouble angle;
        public ErDouble period;
        public ErDouble quotient;
    }
}