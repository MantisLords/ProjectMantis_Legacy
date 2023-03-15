using System.Linq;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace MantisTrials.KLP.Trial_23_Elasticity;



public static class Main_Trail_23_Elasticity
{
    public const double S_ERROR = 0.05;//mm
    public const double L_ERROR = 1.0;//mm
    public const double D_ERROR = 0.01;//mm

    public const double G = 9.81;
    
    public static MantisDocument CurrentDocument { get; private set; }
    public static TableCreator CurrentTableCreator { get; private set; }
    
    public static void Process()
    {
        CurrentDocument = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        CurrentTableCreator = new TableCreator(CurrentDocument);
        
        Part_A_ElasticityOfMaterial.GenerateAll();
        Part_B_ElasticityByProfile.GenerateAll();
        Part_C_L3Depedency.Generate();
        Part_D_CopperWire.Generate();
        
        CurrentDocument.Save("KLP_Trail23_Elasticity_Printout.pdf");


    }
    

}