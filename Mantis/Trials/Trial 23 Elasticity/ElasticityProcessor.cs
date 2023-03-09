using System.Linq;
using Mantis.DocumentEngine;
using MathNet.Numerics;
using MathNet.Numerics.LinearRegression;

namespace Mantis.Trials.Trial_23_Elasticity;



public static class ElasticityProccessor
{
    public static void Process()
    {
        MantisDocument document = new MantisDocument(MantisDocument.PrinterPhysicLibraryUniWue);
        
        //GenABMaterialAndProfile.GenerateABAll(document:document);
        CL3Depedency.GenerateAll(document);
        DCopperWire.GenerateAll(document);
        
        document.Save("Elasticity Printout.pdf");


    }
    

}