using System.Collections.Generic;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRageMath;

namespace IngameScript
{
    internal class DockConfigurationBuilder
    {
        private const string MyConnectorNameKey = "My connector name";
        private const string ConnectorNameKey = "Connector name";
        private const string ConnectorIDKey = "Connector ID";
        private const string ConnectorPositionKey = "Connector position";
        private const string ConnectorForwardKey = "Connector forward";
        private const string ConnectorUpGlobalKey = "Connector up global";
        private const string ConnectorUpLocalKey = "Connector up local";
        private const string ConnectorLeftKey = "Connector left";
        private const string GridIDKey = "Grid ID";
        private const string ConnectorSizeKey = "Connector size";
        private const string LandingSequencesKey = "Landing sequences";
        private const string ArgumentsKey = "Arguments";

        private readonly MyIni _ini;

        public DockConfigurationBuilder(string gridName, MyIni ini)
        {
            _ini = ini;
            GridName = gridName;
        }

        public string GridName { get; private set; }

        public string MyConnectorName
        {
            get
            {
                return _ini.Get(GridName, MyConnectorNameKey).ToString();
            }
            set
            {
                _ini.Set(GridName, MyConnectorNameKey, value);
            }
        }

        public string ConnectorName
        {
            get
            {
                return _ini.Get(GridName, ConnectorNameKey).ToString();
            }
            set
            {
                _ini.Set(GridName, ConnectorNameKey, value);
            }
        }

        public long ConnectorID
        {
            get
            {
                return _ini.Get(GridName, ConnectorIDKey).ToInt64();
            }
            set
            {
                _ini.Set(GridName, ConnectorIDKey, value);
            }
        }

        public Vector3D ConnectorPosition
        {
            get
            {
                Vector3D result;
                Vector3D.TryParse(_ini.Get(GridName, ConnectorPositionKey).ToString(), out result);
                return result;
            }
            set
            {
                _ini.Set(GridName, ConnectorPositionKey, value.ToString());
            }
        }

        public Vector3D ConnectorForward
        {
            get
            {
                Vector3D result;
                Vector3D.TryParse(_ini.Get(GridName, ConnectorForwardKey).ToString(), out result);
                return result;
            }
            set
            {
                _ini.Set(GridName, ConnectorForwardKey, value.ToString());
            }
        }

        public Vector3D ConnectorUpGlobal
        {
            get
            {
                Vector3D result;
                Vector3D.TryParse(_ini.Get(GridName, ConnectorUpGlobalKey).ToString(), out result);
                return result;
            }
            set
            {
                _ini.Set(GridName, ConnectorUpGlobalKey, value.ToString());
            }
        }

        public Vector3D ConnectorUpLocal
        {
            get
            {
                Vector3D result;
                Vector3D.TryParse(_ini.Get(GridName, ConnectorUpLocalKey).ToString(), out result);
                return result;
            }
            set
            {
                _ini.Set(GridName, ConnectorUpLocalKey, value.ToString());
            }
        }

        public Vector3D ConnectorLeft
        {
            get
            {
                Vector3D result;
                Vector3D.TryParse(_ini.Get(GridName, ConnectorLeftKey).ToString(), out result);
                return result;
            }
            set
            {
                _ini.Set(GridName, ConnectorLeftKey, value.ToString());
            }
        }

        public long GridID
        {
            get
            {
                return _ini.Get(GridName, GridIDKey).ToInt64();
            }
            set
            {
                _ini.Set(GridName, GridIDKey, value);
            }
        }

        public double ConnectorSize
        {
            get
            {
                return _ini.Get(GridName, ConnectorSizeKey).ToDouble();
            }
            set
            {
                _ini.Set(GridName, ConnectorSizeKey, value);
            }
        }

        public string LandingSequences
        {
            get
            {
                return _ini.Get(GridName, LandingSequencesKey).ToString();
            }
            set
            {
                _ini.Set(GridName, LandingSequencesKey, value);
            }
        }

        public IEnumerable<string> Arguments
        {
            get
            {
                int argumentsCount = _ini.Get(GridName, ArgumentsKey).ToInt32();
                for (int argumentNumber = 1; argumentNumber <= argumentsCount; argumentNumber++)
                {
                    yield return _ini.Get(GridName, ArgumentKey(argumentNumber)).ToString();
                }
            }
            set 
            {
                int argumentNumber = 1;
                foreach (string argument in value)
                {
                    _ini.Set(GridName, ArgumentKey(argumentNumber), argument);
                }

                _ini.Set(GridName, ArgumentsKey, argumentNumber);
            }
        }

        private static string ArgumentKey(int number)
        {
            return $"Argument{number}";
        }
    }
}
