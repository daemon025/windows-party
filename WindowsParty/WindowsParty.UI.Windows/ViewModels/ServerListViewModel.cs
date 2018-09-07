using System.Collections.Generic;
using WindowsParty.Core.Requests;
using WindowsParty.Core.Responses;
using WindowsParty.Core.Services;
using WindowsParty.UI.Windows.Views;
using Caliburn.Micro;

namespace WindowsParty.UI.Windows.ViewModels
{
    public class ServerListViewModel : Screen
    {
        private readonly IServerService _serverService;
        private readonly IEventAggregator _eventAggregator;
        private IEnumerable<ServerInfo> _servers;

        public ServerListViewModel(IServerService serverService, IEventAggregator eventAggregator)
        {
            _serverService = serverService;
            _eventAggregator = eventAggregator;
        }

        public string Token { get; set; }

        public IEnumerable<ServerInfo> ServerList
        {
            get { return _servers; }
            set
            {
                _servers = value;
                NotifyOfPropertyChange(() => _servers);
            }
        }

        protected override async void OnActivate()
        {
            base.OnActivate();

            var serverResponse = await _serverService.GetServerList(new ServerListRequest() {Token = Token});
            ServerList = serverResponse?.Servers;
            var view = this.GetView() as ServerListView;
            if (view != null)
            {
                view.ServerListControl.ItemsSource = null;
                view.ServerListControl.ItemsSource = ServerList;
            }
        }

        public async void Logout()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new LogoutResponse());
        }
    }
}
