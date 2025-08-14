using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Snowberry.Editor.Cutscenes;

// Basic serializable cutscene representation. Stored under map root element as child: <cutscenes>
internal class CutsceneDefinition {
    public string Id;
    public List<CutsceneNode> Nodes = new();
}

internal abstract class CutsceneNode {
    public string Type; // e.g. talk, walk, turn, spawn, music, anim
    public float Delay; // seconds before executing
    public abstract Dictionary<string, object> Serialize();
    public virtual void Deserialize(Dictionary<string, object> data) {
        if (data.TryGetValue("delay", out var d) && float.TryParse(d.ToString(), out var f)) Delay = f;
    }
}

internal sealed class TalkNode : CutsceneNode {
    public string DialogKey;
    public string Portrait;
    public TalkNode(){ Type = "talk"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"dialog", DialogKey}, {"portrait", Portrait}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("dialog", out var v)) DialogKey = v.ToString(); if(data.TryGetValue("portrait", out var p)) Portrait = p.ToString(); }
}

internal sealed class WalkNode : CutsceneNode {
    public string EntityRef; // entity id or name
    public float DX; // horizontal displacement
    public float Speed = 48f; // pixels per second
    public WalkNode(){ Type = "walk"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"entity", EntityRef}, {"dx", DX}, {"speed", Speed}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("entity", out var e)) EntityRef = e.ToString(); if(data.TryGetValue("dx", out var dx) && float.TryParse(dx.ToString(), out var f)) DX = f; if(data.TryGetValue("speed", out var s) && float.TryParse(s.ToString(), out var sp)) Speed = sp; }
}

internal sealed class TurnNode : CutsceneNode {
    public string EntityRef; // entity id or name
    public bool FaceRight;
    public TurnNode(){ Type = "turn"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"entity", EntityRef}, {"right", FaceRight}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("entity", out var e)) EntityRef = e.ToString(); if(data.TryGetValue("right", out var r) && bool.TryParse(r.ToString(), out var b)) FaceRight = b; }
}

internal sealed class SpawnDummyNode : CutsceneNode {
    public string SpriteId;
    public Vector2 Position;
    public string IdRef;
    public SpawnDummyNode(){ Type = "spawn"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"sprite", SpriteId}, {"x", Position.X}, {"y", Position.Y}, {"id", IdRef}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("sprite", out var s)) SpriteId = s.ToString(); if(data.TryGetValue("x", out var x) && float.TryParse(x.ToString(), out var fx)) Position.X = fx; if(data.TryGetValue("y", out var y) && float.TryParse(y.ToString(), out var fy)) Position.Y = fy; if(data.TryGetValue("id", out var id)) IdRef = id.ToString(); }
}

internal sealed class MusicNode : CutsceneNode {
    public string Event; // event:/ path or empty to stop
    public float FadeTime;
    public MusicNode(){ Type = "music"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"event", Event}, {"fade", FadeTime}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("event", out var e)) Event = e.ToString(); if(data.TryGetValue("fade", out var f) && float.TryParse(f.ToString(), out var ft)) FadeTime = ft; }
}

internal sealed class AnimationNode : CutsceneNode {
    public string EntityRef;
    public string AnimationId;
    public AnimationNode(){ Type = "anim"; }
    public override Dictionary<string, object> Serialize() => new(){ {"type", Type}, {"entity", EntityRef}, {"anim", AnimationId}, {"delay", Delay} };
    public override void Deserialize(Dictionary<string, object> data){ base.Deserialize(data); if(data.TryGetValue("entity", out var e)) EntityRef = e.ToString(); if(data.TryGetValue("anim", out var a)) AnimationId = a.ToString(); }
}

internal static class CutsceneSerialization {
    public static Dictionary<string, object> SerializeCutscene(CutsceneDefinition c) {
        var list = new List<Dictionary<string, object>>();
        foreach(var node in c.Nodes) list.Add(node.Serialize());
        return new(){ {"id", c.Id}, {"nodes", list} };
    }
}
