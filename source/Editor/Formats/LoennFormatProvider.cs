using System.IO;
using Celeste;

namespace Snowberry.Editor.Formats;

// Placeholder: Loenn format (likely .bin with additional metadata or .lua-based definitions)
internal sealed class LoennFormatProvider : IMapFormatProvider {
    public string Id => "loenn";
    public string DisplayName => "Loenn";
    public string[] FileExtensions => new[]{ ".bin" }; // Loenn uses celeste .bin but we may distinguish by companion data
    public bool CanLoad(string path) => Path.GetExtension(path).ToLowerInvariant() == ".bin"; // refine later
    public BinaryPacker.Element Load(string path) => BinaryPacker.FromBinary(path);
    public void Save(BinaryPacker.Element root, string path) {
        using var fs = File.Open(path, FileMode.Create, FileAccess.Write);
        BinaryExporter.ExportInto(root, Path.GetFileName(path), fs);
        // TODO: Write any Loenn companion files if required.
    }
}
