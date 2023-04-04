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
            { 102.0, 157.2 }
        };
        string[] colors = new string[] { "rot", "hellgrün", "dunkelgrün", "türkis", "blau", "violett" };
        double[] waveLengths = new double[] { 643.9, 546.1, 508.6, 480.0, 435.8, 404.7 };
        
        List<Griddaten> data = Initialize(wiknkel,colors,waveLengths);
        data = CalculateWavelengths(data);
        
        CurrentTableCreator.AddTable("Berechnete Wellenlängen",
            new string[]{"Spektrallinie","Winkel / °","Wellenlänge / nm","Literaturwert / nm"},data.Select(e=>new string[]
            {
                e.Color,
                e.Angle.ToString(),
                e.WaveLength.Mul10E(9).ToString(),
                e.TheoWaveLength.ToString()
            }),
            GlobalStyles.StandardTable,times:2);

    }

    public static List<Griddaten> CalculateWavelengths(List<Griddaten> listdata)
    {

        for (int i = 0; i < listdata.Count; i++)
        {
            Griddaten e = listdata[i];
            e.WaveLength = (ErDouble.Sin(listdata[i].Angle*Math.PI/180) * gitterConst / 2);

            listdata[i] = e;
        }

        return listdata;
    }
    public static List<Griddaten> Initialize(double[,] rawData,string[] colors,double[] theoWaveLengths)
    {
        List<Griddaten> data = new List<Griddaten>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new Griddaten()
                {
                    Angle = new ErDouble((rawData[i,1]-rawData[i,0])/2,0.14),
                    Color = colors[i],
                    TheoWaveLength = theoWaveLengths[i]
                }
            );
        }

        return data;
    }
    public struct Griddaten
    {
        public ErDouble Angle;
        public ErDouble WaveLength;
        public double TheoWaveLength;
        public string Color;
    }
}