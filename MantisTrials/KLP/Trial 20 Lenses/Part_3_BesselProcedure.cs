using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_3_BesselProcedure
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] lensData = new double[,]
        {
            { 372, 633 },
            { 370, 630 },
            { 371, 629 }
        };//mm
        double[,] lensDataAbbildungsgleichung = new double[,]
        {
            { 729, 1170 },
            { 725, 1170 },
            { 727, 1170 }
        };

        Double d = 150;//mm
        ErDouble f1 = new ErDouble(198.7, 2.5);
        ErDouble f2 = new ErDouble( -397.13369073546653,3.3);
        List <Part_1_FocalLengthConvexLensAutokummulation.LensData> data = Initialize(lensData,0.3,0.5);
        List<Part_1_FocalLengthConvexLensAutokummulation.LensData> dataAbbilungsgleichung =
            Initialize(lensDataAbbildungsgleichung, 0.5, 0.2);
        ErDouble position1Linse = Part_1_FocalLengthConvexLensAutokummulation.GetMean1(dataAbbilungsgleichung);
        position1Linse.Error = 6;
        ErDouble position2Linse = Part_1_FocalLengthConvexLensAutokummulation.GetMean2(dataAbbilungsgleichung);
        position2Linse.Error = 3;
        ErDouble delta = position2Linse - position1Linse;
        ErDouble e = Main_Trial_20_Lenses.defaultSchirmPostition - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble mean1 = Part_1_FocalLengthConvexLensAutokummulation.GetMean1(data);
        ErDouble mean2 = Part_1_FocalLengthConvexLensAutokummulation.GetMean2(data);
        mean1.Error = 3;
        mean2.Error = 5;
        ErDouble k = mean1 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble l = mean2 - Main_Trial_20_Lenses.defaultObject1Position;
        ErDouble s = k + l;
        ErDouble f = 0.5*((e - s).Pow(2) - delta.Pow(2)).Pow(0.5);
        ErDouble h = s - 2*f;
        ErDouble fTheo = -f1 * f2 / (d - f1 - f2);
        fTheo.Error =
            (fTheo * (((1 / f1 + 1 / (d - f1 - f2)) * f1.Error).Pow(2) +
                      ((1 / f2 + 1 / (d - f1 - f2)) * f2.Error).Pow(2)).Pow(0.5)).Value;
        ErDouble hTheo = d * d / (d - f1 - f2);

        ErDouble g = (e - delta - h) / 2;
        ErDouble b = (e + delta - h) / 2;

        List<SimpleTab> output = new List<SimpleTab>();
        
        output.Add(new SimpleTab("s",s));
        output.Add(new SimpleTab("e",e));
        output.Add(new SimpleTab("Pos1 Dis",position1Linse - Main_Trial_20_Lenses.defaultObject1Position));
        output.Add(new SimpleTab("Pos2 Dis",position2Linse - Main_Trial_20_Lenses.defaultObject1Position));
        output.Add(new SimpleTab("delta",delta));
        output.Add(new SimpleTab("f",f));
        output.Add(new SimpleTab("h",h));
        output.Add(new SimpleTab("g",g));
        output.Add(new SimpleTab("b",b));
        output.Add(new SimpleTab("fTheo",fTheo));
        output.Add(new SimpleTab("hTheo",hTheo));
        output.Add(new SimpleTab("Position1",position1Linse));
        output.Add(new SimpleTab("Position2",position2Linse));

        output = output.Select(e =>
        {
            e.ReScaledValue = e.Value.Value / 50.0;
            return e;
        }).ToList();
        
        CurrentTableCreator.AddTable("Stuff",new []{"","",""},output.Select(e => e.ToTableRow()));
    }

    private struct SimpleTab
    {
        public string Name;
        public ErDouble Value;
        public double ReScaledValue;

        public SimpleTab(string name, ErDouble value) : this()
        {
            Name = name;
            Value = value;
        }

        public string[] ToTableRow()
        {
            return new string[] { Name, Value.ToString(), ReScaledValue.ToString("G4") };
        }
    }

    public static List<Part_1_FocalLengthConvexLensAutokummulation.LensData> Initialize(double[,] rawData,double fehler1, double fehler2)
    {
        List<Part_1_FocalLengthConvexLensAutokummulation.LensData> data =
            new List<Part_1_FocalLengthConvexLensAutokummulation.LensData>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new Part_1_FocalLengthConvexLensAutokummulation.LensData()
                {
                    Position1 = new ErDouble(rawData[i,0],fehler1),
                    Position2 = new ErDouble(rawData[i,1],fehler2)
                }
            );
        }

        return data;
    }
}