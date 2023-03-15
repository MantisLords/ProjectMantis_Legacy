using System.Collections.Generic;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace Mantis.DocumentEngine.TableCreator;

public class TableCreator : FormattedPageCreator
{
    public TableCreator(MantisDocument parent) : base(parent)
    {
        MigraDoc.AddSection();
    }

    public void AddTableRaw(string tableName, string[,] content, TableStyle style)
    {
        MigraDoc.LastSection.AddParagraph();
        Table table = MigraDoc.LastSection.AddTable();

        table.Borders.Width = style.BorderWidth;

        int columnCount = content.GetLength(1);
        int rowCount = content.GetLength(0);

        for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            Column column = table.AddColumn(style.ColumnWidth);
            column.Format.Alignment = ParagraphAlignment.Left;
        }

        table.Rows.Height = style.RowHeight;

        Row nameRow = table.AddRow();
        nameRow.Cells[0].MergeRight = columnCount-1;
        nameRow.Cells[0].AddParagraph(tableName);
        nameRow.VerticalAlignment = VerticalAlignment.Center;

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            Row row = table.AddRow();
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                row.Cells[columnIndex].AddParagraph(content[rowIndex, columnIndex]);
                row.VerticalAlignment = VerticalAlignment.Center;
            }
        }

        table.KeepTogether = false;
    }

    public void AddTable(string tableName, string[] headers, string[][] content)
        => AddTable(tableName, headers, content, GlobalStyles.StandardTable);

    public void AddTable(string tableName, string[] headers, string[][] content, TableStyle style)
    {
        string[,] completeContent = new string[content.Length + 1, headers.Length];

        for (int i = 0; i < headers.Length; i++)
        {
            completeContent[0, i] = headers[i];
        }

        for (int i = 0; i < content.Length; i++)
        {
            for (int j = 0; j < headers.Length && j < content[i].Length; j++)
            {
                completeContent[i + 1, j] = content[i][j];
            }
        }
        
        AddTableRaw(tableName,completeContent,style);
    }

    public void AddTable(string tableName, string[] headers, IEnumerable<string[]> content, int times = 1)
        => AddTable(tableName, headers, content, GlobalStyles.StandardTable, times);

    public void AddTable(string tableName, string[] headers, IEnumerable<string[]> content, TableStyle style,int times = 1)
    {
        for (int i = 0; i < times; i++)
        {
            AddTable(tableName,headers,content.ToArray(),style);
        }
    }
}