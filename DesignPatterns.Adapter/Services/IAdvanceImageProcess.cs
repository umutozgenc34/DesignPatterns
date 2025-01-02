using System.Drawing;

namespace DesignPatterns.Adapter.Services;

public interface IAdvanceImageProcess
{
    void AddWatermarkImage(Stream stream, string text, string filePath, Color color, Color outlineColor);
}
