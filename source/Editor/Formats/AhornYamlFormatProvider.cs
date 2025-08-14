using System.IO;
using Celeste;

namespace Snowberry.Editor.Formats;

// Placeholder: Ahorn YAML parser to be implemented (requires parsing .yaml into BinaryPacker.Element)
internal sealed class AhornYamlFormatProvider : IMapFormatProvider {
    public string Id => "ahorn-yaml";
    public string DisplayName => "Ahorn YAML";
    public string[] FileExtensions => new[]{ ".yaml", ".yml" };
    public bool CanLoad(string path) { var ext = Path.GetExtension(path).ToLowerInvariant(); return ext == ".yaml" || ext == ".yml"; }
    public BinaryPacker.Element Load(string path) {
        // TODO: Implement YAML -> Element conversion. For now throw to indicate unsupported.
        throw new System.NotImplementedException("Ahorn YAML import not yet implemented");
    }
    public void Save(BinaryPacker.Element root, string path) {
        // TODO: Implement Element -> YAML serialization.
        throw new System.NotImplementedException("Ahorn YAML export not yet implemented");
    }
}
