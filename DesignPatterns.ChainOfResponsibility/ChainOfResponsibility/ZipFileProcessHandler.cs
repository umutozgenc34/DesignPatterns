using System.IO.Compression;

namespace DesignPatterns.ChainOfResponsibility.ChainOfResponsibility;

public class ZipFileProcessHandler<T> : Processhandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ZipFileProcessHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<object> handle(object o)
    {
        var excelMemoryStream = o as MemoryStream;
        if (excelMemoryStream == null)
            throw new ArgumentException("Invalid object type. Expected a MemoryStream.");

        excelMemoryStream.Position = 0;

        using (var zipStream = new MemoryStream())
        {
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");
                using (var zipEntry = zipFile.Open())
                {
                    await excelMemoryStream.CopyToAsync(zipEntry);
                }
            }

            zipStream.Position = 0;

            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/zip";
                context.Response.Headers.Add("Content-Disposition", $"attachment; filename={typeof(T).Name}.zip");
                await zipStream.CopyToAsync(context.Response.Body);
            }

            return await base.handle(zipStream);
        }
    }
}
