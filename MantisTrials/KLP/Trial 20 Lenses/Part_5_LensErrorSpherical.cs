using Mantis;
using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;
using MigraDoc.DocumentObjectModel.Shapes.Charts;

namespace MantisTrials.KLP.Trial_20_Lenses;

public class Part_5_LensErrorSpherical
{
    private static MantisDocument CurrentDocument => Main_Trial_20_Lenses.CurrentDocument;
    private static TableCreator CurrentTableCreator => Main_Trial_20_Lenses.CurrentTableCreator;

    public static void Generate()
    {
        double[,] linsenDaten1 = new double[,]
        {
            { 0, 391 ,3},
            { 16, 384 ,3},
            { 24, 374 ,5},
            { 32, 356 ,5},
            { 40, 343 ,10}
        };//mm
        double[,] linsenDaten2 = new double[,]
        {
            { 0, 350 ,3},
            { 16, 349 ,3},
            { 24, 346 ,3},
            { 32, 341 ,5},
            { 40, 335 ,8}//wert für position gefälscht
        };//mm

        List<LinsenDaten> daten1 = Initialize(linsenDaten1);
        List<LinsenDaten> daten2 = Initialize(linsenDaten2);
        for (int i = 0; i < daten1.Count; i++)
        {
            LinsenDaten e = daten1[i];
            LinsenDaten m = daten2[i];
            ErDouble abstand1 = e.Position - Main_Trial_20_Lenses.defaultObject1Position;
            ErDouble abstand2 = m.Position - Main_Trial_20_Lenses.defaultObject1Position;
            e.Abstand = abstand1;
            m.Abstand = abstand2;
            daten1[i] = e;
            daten2[i] = m;
            ErDouble deltaf1 = daten1[0].Abstand - e.Abstand;
            ErDouble deltaf2 = daten2[0].Abstand - m.Abstand;
            e.deltaF = deltaf1;
            m.deltaF = deltaf2;
            daten1[i] = e;
            daten2[i] = m;
        }
        CurrentTableCreator.AddTable("Sphärische Brennweitendifferenz",
            new string[]{"Blendenradius/mm","Delta F //mm"},
            daten1.Select(e=>new string[]{e.Blende.ToString(),e.deltaF.ToString()}),
            GlobalStyles.StandardTable);
        CurrentTableCreator.AddTable("Sphärische Brennweitendifferenz bei gedrehter Linse",
            new string[]{"Blendenradius/mm","Delta F //mm"},
            daten2.Select(e=>new string[]{e.Blende.ToString(),e.deltaF.ToString()}),
            GlobalStyles.StandardTable);
        SketchBook sketchBook = new SketchBook("Brennweitendifferenzen");
        List<DataPoint> points = daten1.Select(e => new DataPoint(e.Blende, e.deltaF)).ToList();
        sketchBook.Add(new DataSetSketch("Brennweitendifferenzen",points));
        List<DataPoint> points2 = daten2.Select(e => new DataPoint(e.Blende, e.deltaF)).ToList();
        sketchBook.Add(new DataSetSketch("Brennweitendifferenzen",points2));
        GraphCreator creator = new GraphCreator(CurrentDocument, sketchBook, LinearAxis.Auto("Blende/mm"),
            LinearAxis.Auto("deltaf/mm"));



    }

    public static List<LinsenDaten> Initialize(double[,] rawData)
    {
        List<LinsenDaten> data = new List<LinsenDaten>();
        for (int i = 0; i < rawData.GetLength(0); i++)
        {
            data.Add(new LinsenDaten()
                {
                    Blende = new ErDouble(rawData[i,0],0),
                    Position = new ErDouble(rawData[i,1],rawData[i,2])
                }
            );
        }

        return data;
    }

    public struct LinsenDaten
    {
        public ErDouble Blende;
        public ErDouble Position;
        public ErDouble Abstand;
        public ErDouble deltaF;
    }
}