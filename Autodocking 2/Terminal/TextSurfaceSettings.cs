using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace IngameScript
{
    internal class TextSurfaceSettings : ITextSurfaceSettings
    {
        public ContentType ContentType { get; set; }

        public string Font { get; set; }

        public float FontSize { get; set; }

        public Color FontColor { get; set; }

        public Color BackgroundColor { get; set; }

        public TextAlignment Alignment { get; set; }

        public float TextPadding { get; set; }

        public void CopyTo(ITextSurfaceSettings other)
        {
            other.ContentType = ContentType;
            other.Font = Font;
            other.FontSize = FontSize;
            other.FontColor = FontColor;
            other.BackgroundColor = BackgroundColor;
            other.Alignment = Alignment;
            other.TextPadding = TextPadding;
        }
    }
}
