using System.IO.Pipes;
using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MantisTrials.KLP.Trial_23_Elasticity;

namespace MantisTrials.KLP.Trial_24_Pandulum;

public class Part_3_PhysicalPendulum
{
    private static MantisDocument CurrentDocument => Main_Trial_24_Pandulum.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trail_23_Elasticity.CurrentTableCreator;

    public static void Generate()
    {
        ErDouble Period = new ErDouble();
        ErDouble a = new ErDouble();
        ErDouble b = new ErDouble();
        
        CurrentTableCreator.Print($"Reduzierte Pendellänge: {(Period*Period*Main_Trial_24_Pandulum.G)/(4*Math.PI*Math.PI)} m");
        CurrentTableCreator.Print($"Geometrische Pendellänge: {2*(a*a+a*b+b*b)/3*(a-b)} m");

    }
}