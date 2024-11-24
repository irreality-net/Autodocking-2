using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    internal class ConfigurationBuilder
    {
        private readonly MyIni _ini;

        public ConfigurationBuilder()
        {
            _ini = new MyIni();
        }

        public ISet<string> GridNames
        {
            get
            {
                List<string> result = new List<string>();
                _ini.GetSections(result);
                return new HashSet<string>(result);
            }
        }

        public bool TryLoad(string data, out MyIniParseResult result)
        {
            return _ini.TryParse(data, out result);
        }

        public DockConfigurationBuilder Dock(string gridName)
        {
            return new DockConfigurationBuilder(gridName, _ini);
        }

        public void DeleteDock(string gridName)
        {
            _ini.DeleteSection(gridName);
        }

        public override string ToString()
        {
            return _ini.ToString();
        }
    }
}
