using Mantis.DocumentEngine;
using Mantis.DocumentEngine.TableCreator;

using System.Linq;

namespace Mantis.Experiments;

public static class SimpleTableExp
{
    public static void CreateExampleTable()
    {
        MantisDocument document = new MantisDocument();

        TableCreator tableCreator = new TableCreator(document);

        string[][] content = new[]
        {
            new string[] { "A", "B" },
            new string[] { "C", "D" }
        };
        
        tableCreator.AddTable(
            tablename:"Tab1: Example",
            headers: new string[]{"s /mm"," l / g"},
            content:content,
            style:GlobalStyles.StandardTable);
        
        document.Save("SimpleTableExp.pdf");
    }
}