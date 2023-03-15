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
        double[] periodenDauer =
        {
            2.960,
            2.959,
            2.963,
            2.960,
            2.961,
            2.960,
            2.960,
            2.958,
            2.963,
            2.962
        };
        var LengthofRod = new ErDouble(2.147, 0.0014);
        var LengthofObject = new ErDouble(0.0049, 0.0014);
        var LengthTogether = LengthofObject / 2 + LengthofRod;

        var data = InitializeDataWithError(periodenDauer);
        var periodenMean = CalculateMean(data);
        CurrentTableCreator.Print($"Periodendauer: {periodenMean} s");
        CurrentTableCreator.Print($"g beträgt: {4 * Math.PI * Math.PI * LengthTogether / periodenMean.Pow(2)}");
    }

    public static List<dataForG> InitializeDataWithError(double[] rawData)
    {
        var data = new List<dataForG>();
        for (var i = 0; i < rawData.Length; i++)
            data.Add(new dataForG
                {
                    period = new ErDouble(rawData[i], Main_Trial_24_Pandulum.LASER_ERROR)
                }
            );

        return data;
    }

    public static ErDouble CalculateMean(List<dataForG> data)
    {
        var sum = new ErDouble(0);
        foreach (var e in data) sum = sum + e.period;

        return sum / 10;
    }

    public struct dataForG
    {
        public ErDouble period;
    }
}