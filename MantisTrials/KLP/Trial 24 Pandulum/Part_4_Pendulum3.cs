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
        
        double[,] messungen = new double[,]
        {
            {10,3.342},
            {10,3.342},
            {19,3.358},
            {19,3.358},
            {29,3.388},
            {29,3.390},
            {38,3.428},
            {37,3.430},
            {47,3.487},
            {47,3.485},
            {56,3.553},
            {56,3.557},
            {66,3.630},
            {66,3.632},
            {75,3.725},
            {75,3.724},
            {85,3.829},
            {85,3.834},
            {94,3.969},
            {93,3.948},
            {103,4.083},
            {103,4.093},
            {112,4.238},
            {112,4.239},
            {119,4.398},
            {119,4.398},
            {125,4.556},
            {125,4.540}
        };//gleich mit Auslenkwinkel nach Schwingung eingeben
        List<Pendulumdata> messungDaten = InitiateDataWithError(messungen);
        List<TheoPendulumData> theoDaten = GenerateTheoreticalTQuotient(130);

        messungDaten = messungDaten.Select(e => CalculateQuotient(e, new ErDouble(3.3335, Main_Trial_24_Pandulum.LASER_ERROR))).ToList();
        
        SketchBook sketchBook = new SketchBook("Pendulum 3");
        List<DataPoint> points = new List<DataPoint>();
        foreach (Pendulumdata e in messungDaten)
        {
            points.Add(new DataPoint(e.angle,e.quotient));
        }
        sketchBook.Add(new DataSetSketch(points));
        List<DataPoint> theoPoints = new List<DataPoint>();
        foreach (TheoPendulumData e in theoDaten)
        {
            points.Add(new DataPoint(e.angleTheo,e.quotientTheo));
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
        
        CurrentTableCreator.AddTable(tablename:"Theoretischer Pendeldatan",
            headers:new string[]{"Phi","Quotient","Iterationen"},
            content:theoDaten.Select(e => new string[]{e.angleTheo.ToString(),e.quotientTheo.ToString("G5"),e.iterations.ToString()}),
            style:GlobalStyles.StandardTable);
        CurrentTableCreator.MigraDoc.LastSection.AddParagraph();
    } 
    private static Pendulumdata CalculateQuotient(Pendulumdata listdata, ErDouble T0)
    {
        listdata.quotient = (listdata.period)/ T0 - 1;
        return listdata;
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
                    angle = new ErDouble(rawData[i, 0], Main_Trial_24_Pandulum.ANGLE_READING_ERROR_SWINGING), 
                                                  
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
         }

    public struct TheoPendulumData
    {
        public double quotientTheo;
        public double angleTheo;
        public int iterations;
    }

    public static List<TheoPendulumData> GenerateTheoreticalTQuotient(int maxPhi)
    {
        (decimal T0,int it) = CalculateT(0, 0.01m);

        List<TheoPendulumData> values = new List<TheoPendulumData>();
        
        for (int phiGrad = 0; phiGrad <= maxPhi; phiGrad+=5)
        {
            decimal phi = (decimal)(Math.PI * phiGrad / 180.0);
            (decimal T,int iterations) = CalculateT(phi, 0.0000000001m);

            decimal Q = T / T0 - 1;
            
            values.Add(new TheoPendulumData{quotientTheo = (double)Q,angleTheo = phiGrad,iterations = iterations});
        }

        return values;
    }

    private static (decimal,int) CalculateT(decimal phi, decimal precision)
    {
        decimal series = 0;
        int iterations = 0;

        decimal sin = (decimal)Math.Sin((double)phi / 2);

        for (int i = 0; i < 1000; i++)
        {
            decimal res = 1.0m;
            for (int j = 1; j < 2*i; j+=2)
            {
                res *= ((decimal)j) / ((decimal)j + 1) * sin;
            }

            res = res * res;
            if (res < precision)
            {
                iterations = i;
                break;
            }

            series += res;
        }

        return (series, iterations);
    }
    
}