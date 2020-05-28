using System;

namespace PlexApps
{
    public class PlexLoginState
    {
        private bool _loggedIn;

        public bool LoggedIn
        {
            get => _loggedIn;
            set
            {
                if (_loggedIn != value)
                {
                    _loggedIn = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}