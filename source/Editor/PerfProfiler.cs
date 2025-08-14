using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;

namespace Snowberry.Editor;

internal static class PerfProfiler {
    private class Sample { public string Name; public long Ticks; }
    private static readonly Stopwatch sw = new();
    private static readonly List<Sample> frame = new();
    private static readonly List<Sample> last = new();
    private static Sample current;

    public static void BeginFrame() {
        frame.Clear();
    }

    public static void Begin(string name) {
        if (!sw.IsRunning) sw.Start();
        current = new Sample { Name = name, Ticks = sw.ElapsedTicks };
    }

    public static void End() {
        if (current == null) return;
        long end = sw.ElapsedTicks;
        frame.Add(new Sample { Name = current.Name, Ticks = end - current.Ticks });
        current = null;
    }

    public static void Commit() {
        last.Clear();
        last.AddRange(frame);
    }

    public static void DrawOverlay() {
        if (!Snowberry.Settings.ShowPerformanceOverlay) return;
        const int x = 8; int y = 8;
        long total = last.Sum(s => s.Ticks);
        foreach (var s in last) {
            float ms = (float)(s.Ticks * 1000.0 / Stopwatch.Frequency);
            float pct = total > 0 ? (s.Ticks / (float)total) * 100f : 0f;
            string txt = $"{s.Name}: {ms:F2}ms ({pct:F1}%)";
            Fonts.Regular.Draw(txt, new Vector2(x, y), Vector2.One, Color.White);
            y += 10;
        }
    }
}
