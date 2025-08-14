using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Snowberry.Editor.Formats;

internal static class MapFormatRegistry {
    private static readonly List<IMapFormatProvider> providers = new();
    private static bool initialized;

    public static IEnumerable<IMapFormatProvider> Providers => providers;

    public static void Register(IMapFormatProvider provider) {
        if (providers.Any(p => p.Id == provider.Id))
            throw new InvalidOperationException($"Duplicate map format id '{provider.Id}'");
        providers.Add(provider);
    Snowberry.Log(Celeste.Mod.LogLevel.Info, $"Registered map format provider '{provider.DisplayName}' ({provider.Id})");
    }

    public static IMapFormatProvider FindForPath(string path) {
        EnsureInit();
        return providers.FirstOrDefault(p => p.CanLoad(path));
    }

    public static IMapFormatProvider FindById(string id) {
        EnsureInit();
        return providers.FirstOrDefault(p => p.Id == id);
    }

    public static void EnsureInit() {
        if (initialized) return;
        initialized = true;
        // Register built-ins
        Register(new CelesteBinFormatProvider());
        Register(new AhornYamlFormatProvider());
        Register(new LoennFormatProvider());
        Register(new IngesteExtendedFormatProvider());
    }
}
