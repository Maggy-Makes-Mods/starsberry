using System.IO;
using Celeste;

namespace Snowberry.Editor.Formats;

// Placeholder: Ingeste extended format adds custom metadata (entities, bosses, HP, etc.)
internal sealed class IngesteExtendedFormatProvider : IMapFormatProvider {
    public string Id => "ingeste-extended";
    public string DisplayName => "Ingeste Extended";
    public string[] FileExtensions => new[]{ ".bin", ".ing" }; // .ing (hypothetical) sidecar for extended data
    public bool CanLoad(string path) {
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return ext == ".ing" || ext == ".bin"; // refine detection if sidecar present
    }
    public BinaryPacker.Element Load(string path) {
        // For now, treat like binary; later, merge sidecar metadata.
        if (Path.GetExtension(path).ToLowerInvariant() == ".bin")
            return BinaryPacker.FromBinary(path);
        // TODO: parse .ing sidecar and merge
        throw new System.NotImplementedException("Ingeste sidecar parsing not implemented");
    }
    public void Save(BinaryPacker.Element root, string path) {
        using var fs = File.Open(path, FileMode.Create, FileAccess.Write);
        BinaryExporter.ExportInto(root, Path.GetFileName(path), fs);
        // TODO: write sidecar metadata file if needed (.ing)
    }
}
