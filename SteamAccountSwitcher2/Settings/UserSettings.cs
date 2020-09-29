using System;

namespace SteamAccountSwitcher2.Settings
{
    public class UserSettings
    {
        private readonly bool _globalSettings;

        private EncryptionType _encryptionType;
        private string _steamInstallDir;
        private bool _autoStart;


        public UserSettings() : this(false)
        {

        }

        public UserSettings(bool global)
        {
            _globalSettings = global;
            if (_globalSettings)
            {
                _autoStart = Properties.Settings.Default.autostart;
                _steamInstallDir = Properties.Settings.Default.steamInstallDir;
                _encryptionType = (EncryptionType) Enum.Parse(typeof(EncryptionType), Properties.Settings.Default.encryption);
            }
        }

        public string SteamInstallDir
        {
            get => _steamInstallDir;
            set
            {
                _steamInstallDir = value; 
                if(_globalSettings)
				{
					Properties.Settings.Default.steamInstallDir = value;
				}
			}
        }

        public EncryptionType EncryptionType
        {
            get => _encryptionType;
            set
            {
                _encryptionType = value;
                if (_globalSettings)
				{
					Properties.Settings.Default.encryption = value.ToString();
				}
			}
        }

        public bool AutoStart
        {
            get => _autoStart;
            set
            {
                _autoStart = value;
                if (_globalSettings)
				{
					Properties.Settings.Default.autostart = value;
				}
			}
        }

        protected bool Equals(UserSettings otherUserSettings)
        {
            return _steamInstallDir == otherUserSettings._steamInstallDir &&
                   _encryptionType == otherUserSettings._encryptionType &&
                   _autoStart == otherUserSettings._autoStart;
        }

        public UserSettings Copy()
        {
            var copySettings = new UserSettings
            {
                AutoStart = AutoStart,
                SteamInstallDir = SteamInstallDir,
                EncryptionType = EncryptionType
            };
            return copySettings;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != this.GetType())
			{
				return false;
			}

			return Equals((UserSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
	            // ReSharper disable NonReadonlyMemberInGetHashCode
	            var hashCode = _steamInstallDir != null ? _steamInstallDir.GetHashCode() : 0;
	            hashCode = (hashCode * 397) ^ _encryptionType.GetHashCode();
                hashCode = (hashCode * 397) ^ _autoStart.GetHashCode();
                // ReSharper restore NonReadonlyMemberInGetHashCode
                return hashCode;
            }
        }
    }
}