using System.IO;
using Celeste;

namespace Snowberry.Editor.Formats;

// Abstraction for multiple map formats (Celeste .bin, Ahorn YAML, Loenn, Ingeste extended, etc.)
public interface IMapFormatProvider {
    string Id { get; }              // unique id (e.g. "celeste-bin", "ahorn-yaml")
    string DisplayName { get; }     // user-facing name
    string[] FileExtensions { get; } // including leading dot

    bool CanLoad(string path);

    // Load into in-memory BinaryPacker.Element tree (canonical internal form)
    Celeste.BinaryPacker.Element Load(string path);

    // Export from canonical internal form
    void Save(Celeste.BinaryPacker.Element root, string path);
}
