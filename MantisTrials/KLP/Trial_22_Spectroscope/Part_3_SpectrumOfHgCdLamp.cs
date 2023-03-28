using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Part_3_SpectrumOfHgCdLamp
{
    private static MantisDocument CurrentDocument => Main_Trial_22_Spectroscope.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_22_Spectroscope.CurrentTableCreator;
    private static ErDouble defaultAngle = new ErDouble(307.8, 0.1);
    private static ErDouble gitterConst = new ErDouble(1.7453, 0.01).Mul10E(-6);

    public static void Generate()
    {
        double[,] wiknkel = new double[,]
        {
            { 79.0, 173.9 },
            { 88.2, 165.4 },
            { 91.4, 162.5 },
            { 93.7, 160.6 },
            { 99.7, 159.6 },
            { 97.3, 157.2 }
        };
        List<Griddaten> data = Initialize(wiknkel);
        List<ErDouble> wellen = CalculateWavelengths(data);
        
        CurrentTableCreator.AddTable("Berechnete Wellenlängen",
            new string[]{"Wellenlänge"},wellen.Select(e=>new string[]{e.ToString()}),
            GlobalStyles.StandardTable);

    }

    public static List<ErDouble> CalculateWavelengths(List<Griddaten> listdata)
    {
        List<ErDouble> data = new List<ErDouble>();
        for (int i = 0; i < listdata.Count; i++)
        {
            data.Add(ErDouble.Sin(listdata[i].Angle*Math.PI/180) * gitterConst / 2);
        }

        return data;
    }
    public static List<Griddaten> Initialize(double[,] rawData)
    {
        List<Griddaten> data = new List<Griddaten>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new Griddaten()
                {
                    Angle = new ErDouble((rawData[i,1]-rawData[i,0])/2,0.14)
                }
            );
        }

        return data;
    }
    public struct Griddaten
    {
        public ErDouble Angle;
    }
}