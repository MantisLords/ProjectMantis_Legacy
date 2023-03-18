using System.Diagnostics;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_25_PohlWheel;

public class Main_Trial_25_PohlWheel
{
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }
    public const double ERROR_PERIODE = 0.001;//s
    public const double ERROR_AMPLITUE = 0.1;//Skaleneinheit
    public const double CURRENT_ERROR = 0.01;
    public const double VOLTAGE_ERROR = 0.1;
    public const double FREQUENCY_ERROR = 0.1;
    

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        
        Homework.Generate();
        Part_1_ResonantFrequency.Generate();
        Part_2_DampingCoefficient.Generate();
        Part_3_ResonanceCurves.Generate();
        BerechneDelta.Generate();
        CurrentDocument.Save("KLP_Trail25_PohlWheel_Printout.pdf");
    }
}