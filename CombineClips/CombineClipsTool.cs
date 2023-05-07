using GBX.NET.Engines.Game;
using GbxToolAPI;

namespace CombineClips;

[ToolName("Combine Clips")]
public class CombineClipsTool : ITool, IHasOutput<CGameCtnMediaClip>
{
    private readonly IEnumerable<CGameCtnMediaClip> clips;

    public CombineClipsTool(IEnumerable<CGameCtnMediaClip> clips)
    {
        this.clips = clips;
    }

    public CGameCtnMediaClip Produce()
    {
        var primaryClip = default(CGameCtnMediaClip);

        foreach (var clip in clips)
        {
            if (primaryClip is not null)
            {
                foreach (var track in clip.Tracks)
                {
                    primaryClip.Tracks.Add(track);
                }
            }

            primaryClip ??= clip;
        }

        return primaryClip ?? throw new Exception("No clips to combine.");
    }
}
