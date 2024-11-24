using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    public class ConsoleBlock : IMyTextSurfaceProvider
    {
        private IMyTerminalBlock _block;
        private IMyTextSurfaceProvider _provider;

        public ConsoleBlock(IMyTerminalBlock block, ConsoleConfigurationBuilder configuration)
        {
            _block = block;
            _provider = (IMyTextSurfaceProvider)block;
            Configuration = configuration;
        }

        public string CustomName
        {
            get
            {
                return _block.CustomName;
            }
        }

        public ConsoleConfigurationBuilder Configuration { get; private set; }

        public bool UseGenericLcd
        {
            get
            {
                return _provider.UseGenericLcd;
            }
        }

        public int SurfaceCount
        {
            get
            {
                return _provider.SurfaceCount;
            }
        }

        public IMyTextSurface GetSurface(int index)
        {
            return _provider.GetSurface(index);
        }

        public static IEnumerable<ConsoleBlock> Find(IEnumerable<IMyTerminalBlock> blocks, string scriptID)
        {
            foreach (IMyTerminalBlock block in blocks.Where(x => x is IMyTextSurfaceProvider))
            {
                ConsoleConfigurationBuilder configuration = new ConsoleConfigurationBuilder();
                if (configuration.TryLoad(block.CustomData) && configuration.Displays.Any(display => display.ScriptID == scriptID))
                {
                    yield return new ConsoleBlock(block, configuration);
                }
            }
        }
    }
}
