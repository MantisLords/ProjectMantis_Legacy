using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MathNet.Numerics.Random;
using MigraDoc.DocumentObjectModel;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class Part_3_ResonanceCurves
{
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator currentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;

    public static void Generate()
    {
        double[,] daten200mA = new double[,]
        {
            {102,-0.108,-0.292},
            {149,-0.071,-0.331},
            {180,0.060,-0.463},
            {193,0.355,-0.759},
            {202,1.050,-1.463},
            {206,1.036,-1.405},
            {212,1.114,-1.529},
            {219,0.437,-0.826},
            {238,-0.023,-0.391},
            {285,-0.105,-0.310,}
        };// frequenz (Hz), A+ (V) ,A- (V)

        ErDouble T0 = new ErDouble(1.902);
        ErDouble W0 = 2 * Math.PI / T0;
        ErDouble A0 = new ErDouble(0.054, 0.001);

        List<ResDaten> resDaten200mA = InitializeData(daten200mA);
        string[] tableContent = new string[resDaten200mA.Count];
        for (int i = 0; i < resDaten200mA.Count; i++)
        {
            ResDaten e = resDaten200mA[i];
           

           e.W = 2 * Math.PI * e.Frequenz / 400;
           e.A = (e.Aplus - e.Aminus) / 2;
           e.WQuotient = e.W / W0;
           e.AQuotient = e.A / A0;
           resDaten200mA[i] = e;
        }
        currentTableCreator.AddTable("Resonanz bei 200mA - Teil 1",
            new string[]{"Frequenz","w","w/w0","A+"},
            resDaten200mA.Select(e=>new string[]{e.Frequenz.ToString(),e.W.ToString(),e.WQuotient.ToString(),e.Aplus.ToString()}),
            GlobalStyles.StandardTable,
            1);
        //currentTableCreator.AddPageBreak();
        currentTableCreator.AddTable("Resonanz bei 200mA - Teil 2",
            new string[]{"A-","A","A/A0"},
            resDaten200mA.Select(e=>new string[]{e.Aminus.ToString(),e.A.ToString(),e.AQuotient.ToString()}),
            GlobalStyles.StandardTable,
            1);
    }

    public static List<ResDaten> InitializeData(double[,] rawData)
    {
        List<ResDaten> data = new List<ResDaten>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new ResDaten()
                {
                    Frequenz = new ErDouble(rawData[i,0],Main_Trial_25_PohlWheel.FREQUENCY_ERROR),
                    Aplus = new ErDouble(rawData[i,1],Main_Trial_25_PohlWheel.VOLTAGE_ERROR),
                    Aminus = new ErDouble(rawData[i,2],Main_Trial_25_PohlWheel.VOLTAGE_ERROR)
                }
            );
        }

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