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

        string[,] content = new string[,] {{"A","B"},{"C","D"}};
        
        tableCreator.AddTable("First Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("2 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("3 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("4 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("5 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("2 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("3 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("4 Table",content,GlobalStyles.StandardTable);
        tableCreator.AddTable("5 Table",content,GlobalStyles.StandardTable);
        
        document.Save("SimpleTableExp.pdf");
    }
}