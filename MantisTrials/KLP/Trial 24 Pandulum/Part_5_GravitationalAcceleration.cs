using System.Drawing;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_5_GravitationalAcceleration
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_24_Pandulum.CurrentTableCreator;

    public static void Generate()
    {
        double[] periodenDauer = new double[]
        {

        };
        ErDouble LengthofRod = new ErDouble();
        ErDouble LengthofObject = new ErDouble();

        List<dataForG> data = InitializeDataWithError(periodenDauer);
        ErDouble periodenMean = CalculateMean(data);
        CurrentTableCreator.Print($"Periodendauer: {periodenDauer} s");
        CurrentTableCreator.Print($"g beträgt: {(4*Math.PI*LengthofRod)/(periodenMean*periodenMean)}");

    }

    public static List<dataForG> InitializeDataWithError(double[] rawData)
    {
        List<dataForG> data = new List<dataForG>();
        for (int i = 0; i < rawData.Length; i++)
        {
            data.Add(new dataForG()
                {
                    period = new ErDouble(rawData[i],Main_Trial_24_Pandulum.LASER_ERROR)
                }
            );
        }

        return data;
    }

    public static ErDouble CalculateMean(List<dataForG> data)
    {
        ErDouble sum = new ErDouble(0, 0);
        foreach (dataForG e in data)
        {
            sum = sum + e.period;
        }

        return sum / 10;
    }
    public struct dataForG
    {
        public ErDouble period;
    }
    
}