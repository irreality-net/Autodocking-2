using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace IngameScript
{
    internal class Display : ITextSurfaceSettings
    {
        public const string DefaultPrompt = "> ";

        private readonly IMyTextSurface _surface;
        private readonly string _subtypeId;
        private readonly Vector2 _surfaceSize;

        public Display(string name, IMyTextSurface surface, string subtypeId)
        {
            Name = name;
            _surface = surface;
            _subtypeId = subtypeId;

            switch (_subtypeId)
            {
                case "LargeBlockCockpitIndustrial":
                case "LargeBlockCockpitSeat":
                case "LargeBlockInsetButtonPanel":
                case "SmallProgrammableBlock":
                case "SmallProgrammableBlockReskin":
                    float scale = 512 / _surface.SurfaceSize.X;
                    _surfaceSize = new Vector2(_surface.SurfaceSize.X * scale, _surface.SurfaceSize.Y * scale);
                    break;

                default:
                    _surfaceSize = _surface.SurfaceSize;
                    break;
            }

            Lines = new List<string>();
            IsLastLineFinished = true;
            Cursor = new Cursor();

            MeasureSize();
        }

        public string Name { get; }

        public string Font
        {
            get
            {
                return _surface.Font;
            }
            set
            {
                _surface.Font = value;
                MeasureSize();
            }
        }

        public float FontSize
        {
            get
            {
                return _surface.FontSize;
            }
            set
            {
                _surface.FontSize = value;
                MeasureSize();
            }
        }

        public float TextPadding
        {
            get
            {
                return _surface.TextPadding;
            }
            set
            {
                _surface.TextPadding = value;
                MeasureSize();
            }
        }

        public ContentType ContentType
        {
            get
            {
                return _surface.ContentType;
            }
            set
            {
                _surface.ContentType = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return _surface.FontColor;
            }
            set
            {
                _surface.FontColor = value;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return _surface.BackgroundColor;
            }
            set
            {
                _surface.BackgroundColor = value;
            }
        }

        public TextAlignment Alignment
        {
            get
            {
                return _surface.Alignment;
            }
            set
            {
                _surface.Alignment = value;
            }
        }

        public int MaxColumnCount { get; private set; }

        public int MaxLineCount { get; private set; }

        public Blinker Cursor { get; set; }

        private List<string> Lines { get; set; }

        private bool IsLastLineFinished { get; set; }

        public void Clear()
        {
            Lines.Clear();
        }

        public void Write(string text)
        {
            AddLines(text.Split(new string[] { "\r\n" }, StringSplitOptions.None));
            IsLastLineFinished = false;
            WriteLines();
        }

        public void WriteLine(string text)
        {
            AddLines(text.Split(new string[] { "\r\n" }, StringSplitOptions.None));
            IsLastLineFinished = true;
            WriteLines();
        }

        public void WriteCommandLine(string text, string prompt = DefaultPrompt, bool isNewLineRequired = false)
        {
            string line = string.Concat(prompt, text, text.Length > 0 ? string.Empty : Cursor.CurrentFrame);
            string whenVisible = string.Concat(prompt, Cursor.WhenVisible);
            string whenNotVisible = string.Concat(prompt, Cursor.WhenNotVisible);

            int lastLineIndex = Lines.Count - 1;
            if (!isNewLineRequired && (lastLineIndex >= 0) && ((Lines[lastLineIndex] == whenVisible) || (Lines[lastLineIndex] == whenNotVisible)))
            {
                Lines[lastLineIndex] = line;
                WriteLines();
            }
            else
            {
                WriteLine(line);
            }
        }

        public void RemoveCommandLine(string prompt = DefaultPrompt)
        {
            string whenVisible = string.Concat(prompt, Cursor.WhenVisible);
            string whenNotVisible = string.Concat(prompt, Cursor.WhenNotVisible);
            int lastLineIndex = Lines.Count - 1;
            if ((lastLineIndex >= 0) && ((Lines[lastLineIndex] == whenVisible) || (Lines[lastLineIndex] == whenNotVisible)))
            {
                Lines.RemoveAt(lastLineIndex);
                WriteLines();
            }
        }

        public void Mirror(Action<string> echo, int maxLineCount = 12)
        {
            foreach (string line in GetLines(maxLineCount))
            {
                echo(line.Replace("[", "[[").Replace("]", "]]"));
            }
        }

        public string GetText()
        {
            return _surface.GetText();
        }

        public IEnumerable<string> GetLines(int maxCount)
        {
            int skip = Math.Max(0, Lines.Count - maxCount);
            return Lines.Skip(skip).Take(maxCount);
        }

        public void AddImage(string id)
        {
            _surface.PreserveAspectRatio = true;
            _surface.ChangeInterval = 1;
            _surface.AddImageToSelection(id, true);
        }

        public void RemoveImage(string id)
        {
            _surface.RemoveImageFromSelection(id, true);
        }

        public TextSurfaceSettings GetRecommendedSettings()
        {
            TextSurfaceSettings result = new TextSurfaceSettings()
            {
                ContentType = ContentType.TEXT_AND_IMAGE,
                Font = "DEBUG",
                FontSize = 0.8f,
                FontColor = new Color(192, 192, 192),
                BackgroundColor = new Color(0, 0, 0),
                Alignment = TextAlignment.LEFT,
                TextPadding = 0.2f
            };

            switch (_subtypeId)
            {
                case "LargeBlockCockpit":
                case "LargeBlockCockpitIndustrial":
                case "LargeBlockInsetButtonPanel":
                case "LargeProgrammableBlock":
                case "OpenCockpitLarge":
                    result.FontSize = 0.6f;
                    break;

                case "LargeBlockCockpitSeat":
                    result.FontSize = 0.75f;
                    break;
            };

            return result;
        }

        public void MeasureSize()
        {
            Vector2 letterSize = _surface.MeasureStringInPixels(new StringBuilder("W"), _surface.Font, _surface.FontSize);
            Vector2 padding = new Vector2(_surfaceSize.X * _surface.TextPadding / 100, _surfaceSize.Y * _surface.TextPadding / 100);
            MaxColumnCount = (int)Math.Floor((_surfaceSize.X - padding.X) / letterSize.X);
            MaxLineCount = (int)Math.Floor((_surfaceSize.Y - padding.Y) / letterSize.Y);
        }

        private void AddLines(IEnumerable<string> newLines)
        {
            if (!newLines.Any())
            {
                return;
            }

            if (IsLastLineFinished)
            {
                Lines.AddRange(newLines);
            }
            else
            {
                if (Lines.Count == 0)
                {
                    Lines.Add(string.Empty);
                }

                Lines[Lines.Count - 1] = string.Concat(Lines.Last(), newLines.First());
                Lines.AddRange(newLines.Skip(1));
            }

            int skip = Math.Max(0, Lines.Count - MaxLineCount);
            Lines = Lines.Skip(skip).Take(MaxLineCount).ToList();
        }

        private void WriteLines()
        {
            _surface.WriteText(string.Join("\r\n", Lines), false);
        }

        public static IEnumerable<IMyTextSurface> Find(IEnumerable<IMyTerminalBlock> blocks, string tag, string scriptID)
        {
            foreach (ConsoleBlock consoleBlock in ConsoleBlock.Find(blocks.Where(x => x.CustomName.Contains(tag)), scriptID))
            {
                foreach (DisplayConfigurationBuilder display in consoleBlock.Configuration.Displays.Where(x => x.ScriptID == scriptID))
                {
                    yield return consoleBlock.GetSurface(display.Index);
                }
            }
        }
    }
}
