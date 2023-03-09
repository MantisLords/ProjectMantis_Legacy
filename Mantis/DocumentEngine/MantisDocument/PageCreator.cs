using PdfSharp.Pdf;

namespace Mantis.DocumentEngine;

public abstract class PageCreator
{
    public MantisDocument Parent { get; }


    public PageCreator(MantisDocument parent)
    {
        Parent = parent;

        parent.AddPageInternal(this);
    }


    public abstract void RenderNewPage(PdfDocument document);
}