using System.IO;

namespace SteamAccountSwitcher2
{
    public class SteamAccount
    {
        private const string ImageFolder = "images";

        public SteamAccount()
        {
            // Empty constructor must exist for the JSON Converter!
        }

        public SteamAccount(string accountName, string password)
        {
            Name = accountName;
            AccountName = accountName;
            Password = password;
            Type = AccountType.Main;
        }

        public SteamAccount(string steamId, string accountName, string personaName, bool rememberPassword,
            bool mostRecent, long timestamp)
        {
            Name = PersonaName = personaName;
            SteamId = steamId;
            AccountName = accountName;
            PersonaName = personaName;
            RememberPassword = rememberPassword;
            MostRecent = mostRecent;
            Timestamp = timestamp;
            Type = AccountType.Main;
        }

        public string SteamId { get; set; }

        public string AccountName { get; set; }

        public string PersonaName { get; set; }

        public bool RememberPassword { get; set; }

        public bool MostRecent { get; set; }

        public long Timestamp { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public AccountType Type { get; set; }

        public string AccountImage => Path.Combine(SasManager.Instance.SteamInstallation.InstallDir, "config\\avatarcache", SteamId + ".png");

        public string BGImage => ImageFolder + "/acc-bg-" + Type.ToString().ToLower() + ".jpg";

        //public bool IsCached => string.IsNullOrEmpty(_password);
        public bool CachedAccount
        {
            get => string.IsNullOrEmpty(Password);
            //set => _cachedAccount = value;
        }

        public string StartParameters()
        {
            return "-login " + AccountName + " " + Password;
        }

        public override string ToString()
        {
            return Name + " (Username: " + AccountName + ")";
        }

        protected bool Equals(SteamAccount other)
        {
            return Name == other.Name && AccountName == other.AccountName && Password == other.Password && Type == other.Type && SteamId == other.SteamId && PersonaName == other.PersonaName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SteamAccount) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AccountName != null ? AccountName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Password != null ? Password.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Type;
                hashCode = (hashCode * 397) ^ (SteamId != null ? SteamId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PersonaName != null ? PersonaName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}