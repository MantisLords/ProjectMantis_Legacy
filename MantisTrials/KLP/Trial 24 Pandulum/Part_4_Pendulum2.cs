using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
using MantisTrials.KLP.Trial_23_Elasticity;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_4_Pendulum2
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;

    public static void Generate()
    {
        double[,] hausaufgabe = new double[,]
        {
            { 5, 0.000476 },
            { 10, 0.00191 },
            { 15, 0.00430 },
            { 20, 0.00768 },
            { 25, 0.0121 },
            { 30, 0.0175 },
            { 35, 0.0240 },
            { 40, 0.0317 },
            { 45, 0.0408 },
            { 50, 0.0512 },
            { 55, 0.0634 },
            { 60, 0.0776 },
            { 70, 0.113 },
            { 80, 0.162 },
            { 90, 0.231 },
            { 100, 0.328 },
            { 110, 0.470 },
            { 120, 0.680 },
            { 130, 0.997 }
        };
        double[,] rawData = new double[,]
        {
            {0,1.704},
            {0,1.705},
            {5,1.704},
            {5,1.708},
            {10,1.706},
            {10,1.712},
            {15,1.717},
            {15,1.706},
            {20,1.712},
            {20,1.717},
            {25,1.724},
            {25,1.722},
            {30,1.731},
            {30,1.732},
            {35,1.741},
            {35,1.743},
            {40,1.756},
            {40,1.753},
            {45,1.774},
            {45,1.770},
            {50,1.786},
            {50,1.787},
            {55,1.800},
            {55,1.799},
            {60,1.822},
            {60,1.821}
        };

        List<Pendulumdata> messungDaten = InitiateDataWithError(rawData);
        
        messungDaten = messungDaten.Select(e => CalculateQuotient(e, new ErDouble(1.704, Main_Trial_24_Pandulum.LASER_ERROR))).ToList();
        
        // SketchBook sketchBook = new SketchBook("Pendulum 2");
        // List<DataPoint> points = new List<DataPoint>();
        // foreach (Pendulumdata e in messungDaten)
        // {
        //     points.Add(new DataPoint(e.angle,e.quotient));
        // }
        // sketchBook.Add(new DataSetSketch(points));
        // GraphCreator creator = new GraphCreator(CurrentDocument,
        //     sketchBook,
        //     LinearAxis.Auto("Phi"),
        //     LinearAxis.Auto("quotient"),
        //     GraphOrientation.Portrait);
        
        string[][] Tabelle = new string[messungDaten.Count][];
        for (int i = 0; i < messungDaten.Count; i++)
        {
            Pendulumdata e = messungDaten[i];
            Tabelle[i] = new string[] { e.angle.ToString(), e.quotient.ToString() };
        }
        
        CurrentTableCreator.AddTable(tablename:"Pendeldaten",
            headers: new string[]{"Phi","Quotient"},
            content: Tabelle, //data.Select(e => new string[]{e.epsilon.ToString(),e.sigma.Mul10E(-8).ToString()}).ToArray(),
            style:GlobalStyles.StandardTable,
            times:1);
    }

    private static List<Pendulumdata> InitiateDataWithError(double[,] rawData)
    {
        List<Pendulumdata> data = new List<Pendulumdata>();
        for (int i = 0; i < rawData.GetLength(0); i=i+2)
        {
            data.Add(new Pendulumdata()
                {
                    angle = new ErDouble(rawData[i/2, 0], Main_Trial_24_Pandulum.ANGLE_READING_ERROR),
                    period = new ErDouble((rawData[i,1]+rawData[i-1,1])/2, 7.1*Math.Pow(10,-4))
                });
        }

        return data;
    }

    private static Pendulumdata CalculateQuotient(Pendulumdata listdata, ErDouble T0)
    {
        listdata.quotient = (T0 - listdata.period) / T0;
        return listdata;
    }
    private struct Pendulumdata
    {
        public ErDouble angle;
        public ErDouble period;
        public ErDouble quotient;
    }
}