using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace IngameScript
{
    internal interface ITextSurfaceSettings
    {
        ContentType ContentType { get; set; }

        string Font { get; set; }

        float FontSize { get; set; }

        Color FontColor { get; set; }

        Color BackgroundColor { get; set; }

        TextAlignment Alignment { get; set; }

        float TextPadding { get; set; }
    }
}
