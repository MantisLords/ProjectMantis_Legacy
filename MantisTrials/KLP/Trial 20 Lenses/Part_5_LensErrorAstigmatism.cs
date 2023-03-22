using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_5_LensErrorAstigmatism
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        ErDouble lensPosition = new ErDouble(400, 5);
        ErDouble meridionalPosition = new ErDouble(893, 5);
        ErDouble saggitalPosition = new ErDouble(904, 3);
        ErDouble bm = meridionalPosition - lensPosition;
        ErDouble bs = saggitalPosition - lensPosition;
        CurrentTableCreator.Print($"bs = {bs}");
        CurrentTableCreator.Print($"bm = {bm}");
        CurrentTableCreator.Print($"DeltaAstigm = {bs-bm}");
    }
}