using System;
using WindowsParty.Core.Services;
using Caliburn.Micro;

namespace WindowsParty.UI.Windows.ViewModels
{
    public class ServerListViewModel : Screen
    {
        private readonly IServerService _serverService;

        public ServerListViewModel(IServerService serverService)
        {
            _serverService = serverService;
        }

        public string Token { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            // get server list
        }
    }
}
