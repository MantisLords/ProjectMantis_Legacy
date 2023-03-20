using System.Diagnostics;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Main_Trial_20_Lenses
{
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }
    public static ErDouble defaultObject1Position = new ErDouble(238, 1);//mm
    public static ErDouble defaultSchirmPostition = new ErDouble(1401, 1); //mm

    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        Part_1_FocalLengthKonvexLensClassical.Generate();
        Part_1_FocalLengthConvexLensAutokummulation.Generate();
        Part_2_FocalLengthConcaveLens.Generate();
        Part_3_BesselProcedure.Generate();
        Part_4_FocalLengthConcaveMirror.Generate();
        CurrentDocument.Save("KLP_Trial20_Lenses.pdf");
    }
}