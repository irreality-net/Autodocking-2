using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    internal class ConfigurationBuilder
    {
        public const string DefaultYourTitle = "Captain";
        public const bool DefaultRotateSmallShipOnConnector = true;
        public const bool DefaultRotateLargeShipOnConnector = false;
        public const bool DefaultRotateOnApproach = false;
        public const bool DefaultExtraSoftLandingMode = false;
        public const int DefaultUndockingDistance = 300; // meters

        private const string YourTitleKeyName = "Your title";
        private const string RotateSmallShipOnConnectorKeyName = "Rotate small ship on connector";
        private const string RotateLargeShipOnConnectorKeyName = "Rotate large ship on connector";
        private const string RotateOnApproachKeyName = "Rotate on approach";
        private const string ExtraSoftLandingModeKeyName = "Extra soft landing mode";
        private const string UndockingDistanceKeyName = "Undocking distance";

        private readonly string _sectionName;
        private readonly MyIni _ini;

        public ConfigurationBuilder(string sectionName)
        {
            _sectionName = sectionName;
            _ini = new MyIni();
        }

        public string YourTitle
        {
            get
            {
                return _ini.Get(_sectionName, YourTitleKeyName).ToString(DefaultYourTitle);
            }
            set
            {
                _ini.Set(_sectionName, YourTitleKeyName, value);
            }
        }

        public bool RotateSmallShipOnConnector
        {
            get
            {
                return _ini.Get(_sectionName, RotateSmallShipOnConnectorKeyName).ToBoolean(DefaultRotateSmallShipOnConnector);
            }
            set
            {
                _ini.Set(_sectionName, RotateSmallShipOnConnectorKeyName, value);
            }
        }

        public bool RotateLargeShipOnConnector
        {
            get
            {
                return _ini.Get(_sectionName, RotateLargeShipOnConnectorKeyName).ToBoolean(DefaultRotateLargeShipOnConnector);
            }
            set
            {
                _ini.Set(_sectionName, RotateLargeShipOnConnectorKeyName, value);
            }
        }

        public bool RotateOnApproach
        {
            get
            {
                return _ini.Get(_sectionName, RotateOnApproachKeyName).ToBoolean(DefaultRotateOnApproach);
            }
            set
            {
                _ini.Set(_sectionName, RotateOnApproachKeyName, value);
            }
        }

        public bool ExtraSoftLandingMode
        {
            get
            {
                return _ini.Get(_sectionName, ExtraSoftLandingModeKeyName).ToBoolean(DefaultExtraSoftLandingMode);
            }
            set
            {
                _ini.Set(_sectionName, ExtraSoftLandingModeKeyName, value);
            }
        }

        public int UndockingDistance
        {
            get
            {
                return _ini.Get(_sectionName, UndockingDistanceKeyName).ToInt32(DefaultUndockingDistance);
            }
            set
            {
                _ini.Set(_sectionName, UndockingDistanceKeyName, value);
            }
        }

        public ISet<string> GridNames
        {
            get
            {
                List<string> result = new List<string>();
                _ini.GetSections(result);
                return new HashSet<string>(result.Where(x => x != _sectionName));
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
            // Save all default values
            YourTitle = YourTitle;
            RotateSmallShipOnConnector = RotateSmallShipOnConnector;
            RotateLargeShipOnConnector = RotateLargeShipOnConnector;
            RotateOnApproach = RotateOnApproach;
            ExtraSoftLandingMode = ExtraSoftLandingMode;
            UndockingDistance = UndockingDistance;
            return _ini.ToString();
        }
    }
}
