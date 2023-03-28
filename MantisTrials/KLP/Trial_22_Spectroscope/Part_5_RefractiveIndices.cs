using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

namespace MantisTrials.KLP.Trial_22_Spectroscope;

public class Part_5_RefractiveIndices
{
    private static MantisDocument CurrentDocument => Main_Trial_22_Spectroscope.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_22_Spectroscope.CurrentTableCreator;
    public static ErDouble defaultPrism1 = new ErDouble(315.7, 0.14);
    public static ErDouble defaultPrism2 = new ErDouble(316.9, 0.14);
    public static ErDouble defaultPrism3 = new ErDouble(322.3, 0.707);

    public static double[] wavelengths = new double[]
    {
        643.9, 579.1, 577.0, 546.1, 508.6, 480.0, 467.8, 435.8, 404.7
    };

    public static void Generate()
    {
        double[] dataPrism1 = new double[]
        {
            273.7,
            273.1, 273.0, 272.5, 271.9, 271.1, 270.8, 269.5, 267.7
        };
        double[] dataPrism2 = new double[]
        {
            269.1,
            268.6,
            268.5,
            268.2,
            267.9,
            267.3,
            267.1,
            266.4,
            265.5
        };
        double[] dataPrism3 = new double[]
        {
            264.1,
            262.3,
            262.2,
            262.0,
            261.0,
            260.2,
            259.7,
            258.8,
            256.0
        };
        List<PrismData> data = Initialize(dataPrism1, dataPrism2, dataPrism3);
        CurrentTableCreator.AddTable("Brechzahlen verschiedener Prismen",
            new string[]{"Wellenlänge / nm","Prisma1 / °","Prisma2 / °","Prisma3 / °"},
            data.Select(e=>new string[]{e.Wavelength.ToString(),e.Prism1.ToString(),e.Prism2.ToString(),e.Prism3.ToString()}),
            GlobalStyles.StandardTable,times:2);

        SketchBook sketchBook1 = new SketchBook("Brechungszahlen vershiedener Prismen");
        List<DataPoint> points1 = data.Select(e => new DataPoint(e.Wavelength,e.Prism1)).ToList();
        sketchBook1.Add(new DataSetSketch("Prisma1",points1));
        GraphCreator creator1 = new GraphCreator(CurrentDocument, sketchBook1, xAxis: LinearAxis.Auto("Lamda",400),
            yAxis: LinearAxis.Auto("y",41),
                GraphOrientation.Landscape);
        
        SketchBook sketchBook2 = new SketchBook("Brechungszahlen vershiedener Prismen");
        List<DataPoint> points2 = data.Select(e => new DataPoint(e.Wavelength,e.Prism2)).ToList();
        sketchBook2.Add(new DataSetSketch("Prisma2",points2));
        GraphCreator creator2 = new GraphCreator(CurrentDocument, sketchBook2, xAxis: LinearAxis.Auto("Lamda",400),
            yAxis: LinearAxis.Auto("y",47),
            GraphOrientation.Landscape);
        
        SketchBook sketchBook3 = new SketchBook("Brechungszahlen vershiedener Prismen");
        List<DataPoint> points3 = data.Select(e => new DataPoint(e.Wavelength,e.Prism3)).ToList();
        sketchBook3.Add(new DataSetSketch("Prisma3",points3));
        GraphCreator creator3 = new GraphCreator(CurrentDocument, sketchBook3, xAxis: LinearAxis.Auto("Lamda",400),
            yAxis: LinearAxis.Auto("y",57),
            GraphOrientation.Landscape);

        GenerateIndexTable(data);
    }

    public static ErDouble CalculateRefractiveIndex(ErDouble angle)
    {
        return ErDouble.Sin((60*Math.PI/180 + angle*Math.PI/180) / 2)*2;
    }

    public static void GenerateIndexTable(List<PrismData> listdata)
    {
        List<string[]> dataForTable = new List<string[]>();
        for (int i = 0; i < listdata.Count; i++)
        {
            dataForTable.Add(new string[]
            {
                listdata[i].Wavelength.ToString(),
                CalculateRefractiveIndex(listdata[i].Prism1).ToString(),
                CalculateRefractiveIndex(listdata[i].Prism2).ToString(),
                CalculateRefractiveIndex(listdata[i].Prism3).ToString()
            });
        }
        CurrentTableCreator.AddTable("Brechungsidizes",
            new string[]{"Wellenlänge","Prisma 1","Prisma 2","Prisma 3"},
            dataForTable,
            GlobalStyles.StandardTable,times: 2);
    }

    public static List<PrismData> Initialize(double[] rawData1, double[] rawData2, double[] rawData3)
    {
        List < PrismData > data = new List<PrismData>();
        for (int i = 0; i < rawData1.Length; i++)
        {
            data.Add(new PrismData()
                {
                    Prism1 = defaultPrism1-rawData1[i],
                    Prism2 = defaultPrism2-rawData2[i],
                    Prism3 = defaultPrism3-rawData3[i],
                    Wavelength = wavelengths[i]
                }
            );
        }

        return data;
    }
    public struct PrismData
    {
        public ErDouble Prism1;
        public ErDouble Prism2;
        public ErDouble Prism3;
        public ErDouble Wavelength;
    }
}