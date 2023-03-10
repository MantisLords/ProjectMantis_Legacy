using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using Mantis.Utility;
using MigraDoc.DocumentObjectModel;

namespace MantisTrials.KLP.Trial_23_Elasticity;

public static class Part_B_ElasticityByProfile
{
    public static void GenerateAll()
    {
        
        GenBIronRectangular();
        GenBIronRoundWithHole();
    }

    public static void GenBIronRoundWithHole()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,22.45},
            {20.0664,22.09},
            {19.7206,21.64},
            {19.7826,21.26},
            {19.8744,20.88},
            {19.8662,20.46},
            {19.9110,20.10},
            {20.0022,19.64},
            {19.9663,19.24},
            {19.7058,18.88},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(22.88,Main_Trail_23_Elasticity.L_ERROR); // mm

        ErDouble l = new ErDouble(875, Main_Trail_23_Elasticity.L_ERROR); // mm
        
        ErDouble d = new ErDouble(5.08, Main_Trail_23_Elasticity.D_ERROR); // mm
        ErDouble D = new ErDouble(5.92, Main_Trail_23_Elasticity.D_ERROR); // mm

        ErDouble I =Part_A_ElasticityOfMaterial.GetGeoMomentOfInertiaRodWithHole(d.Mul10E(-3),D.Mul10E(-3)); // m^4
        
        Part_A_ElasticityOfMaterial.Generate(rawData,I,defaultLength,l,"B.2 Stahl Rohrprofil",
            "Tab 13: B.2 Stahl Rohrprofil");
    }
    
    public static void GenBIronRectangular()
    {
        double[,] rawData = new double[,]
        {
            {19.7777,22.15},
            {20.0664,21.41},
            {19.7206,20.73},
            {19.7826,19.97},
            {19.8744,19.20},
            {19.8662,18.38},
            {19.9110,17.64},
            {20.0022,16.85},
            {19.9663,16.11},
            {19.7058,15.42},
        }; // m in g,s in mm
        
        ErDouble defaultLength = new ErDouble(24.00,Main_Trail_23_Elasticity.S_ERROR); // mm

        ErDouble l = new ErDouble(875, Main_Trail_23_Elasticity.L_ERROR); // mm
        
        ErDouble b = new ErDouble(7.89, Main_Trail_23_Elasticity.D_ERROR); // mm
        ErDouble h = new ErDouble(2.88, Main_Trail_23_Elasticity.D_ERROR); // mm

        ErDouble I = Part_A_ElasticityOfMaterial.GetGeoMomentOfInertiaRectangular(b.Mul10E(-3),h.Mul10E(-3)); // m^4
        
        Part_A_ElasticityOfMaterial.Generate(rawData,I,defaultLength,l,"B.1 Stahl Rechteckprofil",
            "Tab 12: B.1 Stahl Rechteckprofil");
    }
}

public struct ElaData
{
    public ErDouble s; // mm
    public ErDouble deltaS; // mm
    public ErDouble m; // g
    public ErDouble F; // N
    
}