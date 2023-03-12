using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public static class Main_Trial_24_Pandulum
{
    public const double REACTION_ERROR = 0.2;// s
    public const double LASER_ERROR = 0.001;//s
    public const double ELECTRO_ERROR = 0.01;//s
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        //Part_1_Stoppwatch.GenerateBoth();
        Part_2_Objective_Measurement.Generate();
        CurrentDocument.Save("KLP_Trail24_Pendulum_Printout.pdf");
    }
}