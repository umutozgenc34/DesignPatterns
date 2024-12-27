using DinkToPdf;
using DinkToPdf.Contracts;
using System.Text;

namespace DesignPatterns.Command.Commands;

public class PdfFile<T>
{
    public readonly List<T> _list;
    public readonly HttpContext _context;

    public PdfFile(List<T> list, HttpContext context)
    {
        _list = list;
        _context = context;
    }

    public string FileName => $"{typeof(T).Name}.pdf";
    public string FileType => "application/octet-stream";

    public MemoryStream Create()
    {
        var type = typeof(T);

        var sb = new StringBuilder();

        sb.Append($@"<html>
                          <head></head>
                          <body>
                            <div class='text-center'><h1>{type.Name} tablo</h1></div>
                            <table class='table table-striped' align='center'>");

        sb.Append("<tr>");
        type.GetProperties().ToList().ForEach(x =>
        {
            sb.Append($"<th>{x.Name}</th>");
        });
        sb.Append("</tr>");

        _list.ForEach(x =>
        {
            var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(x, null)).ToList();

            sb.Append("<tr>");

            values.ForEach(value =>
            {
                sb.Append($"<td>{value}</td>");
            });

            sb.Append("</tr>");
        });

        sb.Append("</table></body></html");

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                        ColorMode = DinkToPdf.ColorMode.Color,
                        Orientation = DinkToPdf.Orientation.Portrait,
                        PaperSize = DinkToPdf.PaperKind.A4,
    },
            Objects = {
                new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = sb.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
        };

        var converter = _context.RequestServices.GetRequiredService<IConverter>();

        return new(converter.Convert(doc));
    }
}
