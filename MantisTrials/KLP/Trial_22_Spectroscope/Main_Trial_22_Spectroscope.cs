using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Main_Trial_22_Spectroscope
{
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        Part_1_GridConstant.Generate();
        Part_5_RefractiveIndices.Generate();
        Part_3_SpectrumOfHgCdLamp.Generate();
        Part_4_PrismAngle.Generate();
        Part_6_Resolution.Generate();
        CurrentDocument.Save("KLP_Trial_22_Spectroscope_Printout.pdf");
        
    }
}