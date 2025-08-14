using Celeste.Mod;

namespace Snowberry; // keep existing namespace to avoid mass refactor yet

// Forward-facing renamed module exposing same functionality while old 'Snowberry' name continues to work.
public sealed class Starsberry : Snowberry {
    public Starsberry() : base() { }
}
