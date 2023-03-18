using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class BerechneDelta
{
    private static MantisDocument CurrentDocument => Main_Trial_25_PohlWheel.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_25_PohlWheel.CurrentTableCreator;
    public static ErDouble wo = new ErDouble(3.3476, 0.0058);
    public static void Generate()
    {
        ErDouble A200 = new ErDouble(41.1, 2.1);
        ErDouble A400Theo = new ErDouble(10.73,0.001);
        ErDouble A400Exp = new ErDouble(11.36, 0.59);
        
        CurrentTableCreator.Print($"Delta bei 200mA:{Berechne(A200).ToString()}");
        CurrentTableCreator.Print($"Theoretiches Delta bei 400mA:{Berechne(A400Theo).ToString()}");
        CurrentTableCreator.Print($"Delta bei 400mA:{Berechne(A400Exp).ToString()}");
    }

    public static ErDouble Berechne(ErDouble wert)
    {
        return (wo * 0.5 ) / wert;
    }
    
}