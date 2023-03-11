using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public static class Main_Trial_24_Pandulum
{
    public const double REACTION_ERROR = 0.2;// s
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        Part_1_Stoppwatch.GenSalah();
        CurrentDocument.Save("KLP_Trail24_Pendulum_Printout.pdf");
    }
}