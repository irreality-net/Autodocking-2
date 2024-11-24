using System;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    public class ConsoleConfigurationBuilder
    {
        private readonly string _defaultScriptID;

        public ConsoleConfigurationBuilder(string defaultScriptID = "AutoLCD")
        {
            Displays = new List<DisplayConfigurationBuilder>();
            _defaultScriptID = defaultScriptID;
        }

        public List<DisplayConfigurationBuilder> Displays { get; set; }

        public bool TryLoad(string data)
        {
            List<string> sections = data
                .Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(section => section.Trim())
                .ToList();

            List<DisplayConfigurationBuilder> displays = new List<DisplayConfigurationBuilder>();
            foreach (string section in sections)
            {
                IEnumerable<string> lines = section
                    .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.Trim());

                string firstLine = lines.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(firstLine))
                {
                    List<string> segments = firstLine
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(segment => segment.Trim())
                        .ToList();

                    int index;
                    if (segments.Count >= 2 && int.TryParse(segments.First(), out index))
                    {
                        displays.Add(new DisplayConfigurationBuilder()
                        {
                            Index = index,
                            ScriptID = string.Join(" ", segments.Skip(1)),
                            Lines = lines.Skip(1).ToList()
                        });
                    }
                    else
                    {
                        if (sections.Count == 1)
                        {
                            displays.Add(new DisplayConfigurationBuilder()
                            {
                                Index = 0,
                                ScriptID = _defaultScriptID,
                                Lines = data
                                    .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(line => line.Trim())
                                    .ToList()
                            });
                        }
                    }
                }
            }

            Displays = displays;

            return true;
        }
    }
}
