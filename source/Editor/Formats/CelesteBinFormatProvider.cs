using System.IO;
using Celeste;

namespace Snowberry.Editor.Formats;

internal sealed class CelesteBinFormatProvider : IMapFormatProvider {
    public string Id => "celeste-bin";
    public string DisplayName => "Celeste Binary (.bin)";
    public string[] FileExtensions => new[]{ ".bin" };
    public bool CanLoad(string path) => Path.GetExtension(path).ToLowerInvariant() == ".bin";
    public BinaryPacker.Element Load(string path) => BinaryPacker.FromBinary(path);
    public void Save(BinaryPacker.Element root, string path) {
        using var fs = File.Open(path, FileMode.Create, FileAccess.Write);
        BinaryExporter.ExportInto(root, Path.GetFileName(path), fs);
    }
}
