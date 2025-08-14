using Celeste;

namespace Snowberry;

// Provides backward compatibility for dialog keys while transitioning SNOWBERRY_* -> STARSBERRY_*
internal static class DialogKeyCompat {
    public static string Clean(string key) {
        // First try legacy key directly
        if (Dialog.Has(key))
            return Dialog.Clean(key);
        // If starts with old prefix, attempt new prefix
        if (key.StartsWith("SNOWBERRY_")) {
            string alt = "STARSBERRY_" + key.Substring("SNOWBERRY_".Length);
            if (Dialog.Has(alt))
                return Dialog.Clean(alt);
        }
        // Fallback (will raise missing key if not found just like original)
        return Dialog.Clean(key);
    }
}
