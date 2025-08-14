using System.Collections.Generic;
using System.Linq;

namespace Snowberry.Editor.Cutscenes;

internal static class CutsceneManager {
    // Store per-map cutscene definitions (keyed by map instance)
    private static readonly Dictionary<Map, List<CutsceneDefinition>> mapCutscenes = new();

    public static bool HasCutscenes(Map map) => mapCutscenes.TryGetValue(map, out var list) && list.Count > 0;
    public static IEnumerable<CutsceneDefinition> GetCutscenes(Map map) => mapCutscenes.TryGetValue(map, out var list) ? list : Enumerable.Empty<CutsceneDefinition>();

    public static CutsceneDefinition CreateCutscene(Map map, string id) {
        var cs = new CutsceneDefinition { Id = id };
        if(!mapCutscenes.TryGetValue(map, out var list)) {
            list = new List<CutsceneDefinition>();
            mapCutscenes[map] = list;
        }
        list.Add(cs);
        return cs;
    }

    public static void Clear(Map map) { mapCutscenes.Remove(map); }
}
