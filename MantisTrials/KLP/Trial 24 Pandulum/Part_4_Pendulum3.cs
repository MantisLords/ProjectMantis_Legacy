using System.Drawing;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_4_Pendulum3
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

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
        double[,] messungen = new double[,]
        {
            { }
        };//gleich mit Auslenkwinkel nach Schwingung eingeben
        List<Pendulumdata> messungDaten = InitiateDataWithError(messungen);
        List<Pendulumdata> theoDaten = InitializeTheo(hausaufgabe);

        messungDaten = messungDaten.Select(e => CalculateQuotient(e, new ErDouble(3.320, Main_Trial_24_Pandulum.LASER_ERROR))).ToList();
        
        SketchBook sketchBook = new SketchBook("Pendulum 3");
        List<DataPoint> points = new List<DataPoint>();
        foreach (Pendulumdata e in messungDaten)
        {
            points.Add(new DataPoint(e.angle,e.quotient));
        }
        sketchBook.Add(new DataSetSketch(points));
        List<DataPoint> theoPoints = new List<DataPoint>();
        foreach (Pendulumdata e in theoDaten)
        {
            points.Add(new DataPoint(e.angle,e.quotientTheo));
        }
        GraphCreator creator = new GraphCreator(CurrentDocument,
            sketchBook,
            LinearAxis.Auto("Phi"),
            LinearAxis.Auto("quotient"),
            GraphOrientation.Portrait);
        
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
    private static Pendulumdata CalculateQuotient(Pendulumdata listdata, ErDouble T0)
    {
        listdata.quotient = (listdata.period-T0) / T0;
        return listdata;
    }
    private static List<Pendulumdata> InitializeTheo(double[,] rawData)
    {
        List<Pendulumdata> data = new List<Pendulumdata>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            
            data.Add(new Pendulumdata()
            {
                angle = new ErDouble(rawData[i,0],0),
                quotientTheo = new ErDouble(rawData[i, 1], 0)
            });
        }

        return data;
    }
    private static List<Pendulumdata> InitiateDataWithError(double[,] rawData)
    {
        List<Pendulumdata> data = new List<Pendulumdata>();
        for (int i = 0; i < rawData.GetLength(0); i+=2)
        {
            if(i<rawData.GetLength(0)-1)
            {
                data.Add(new Pendulumdata()
                {
                    angle = new ErDouble(rawData[i, 0], Main_Trial_24_Pandulum.ANGLE_READING_ERROR), 
                                                  
                    period = new ErDouble((rawData[i, 1]+rawData[i+1,1])/2, 7.1 * Math.Pow(10, -4))
                }); 
            }
                   
        }

        return data;
    }


    private struct Pendulumdata
         {
             public ErDouble angle;
             public ErDouble period;
             public ErDouble quotient;
             public ErDouble quotientTheo;
         }
    
}