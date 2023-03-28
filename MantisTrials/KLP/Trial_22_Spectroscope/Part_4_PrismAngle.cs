using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Part_4_PrismAngle
{
    private static MantisDocument CurrentDocument => Main_Trial_22_Spectroscope.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_22_Spectroscope.CurrentTableCreator;
    public static ErDouble phi1 = new ErDouble(145.0, 0.3);
    public static ErDouble phi2 = new ErDouble(264.7, 0.3);

    public static void Generate()
    {
        CurrentTableCreator.Print($"Epsilon = {CalculateEpsilon()}");
    }

    public static ErDouble CalculateEpsilon()
    {
        return (phi2 - phi1) / 2;
    }
}