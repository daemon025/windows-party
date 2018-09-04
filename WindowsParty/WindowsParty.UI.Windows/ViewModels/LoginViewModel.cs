using System;
using WindowsParty.Core.Requests;
using Caliburn.Micro;
using MediatR;

namespace WindowsParty.UI.Windows.ViewModels
{
    public class LoginViewModel : Screen
    {
        private readonly IMediator _mediator;

        private bool _canLogin;
        private string _password;
        private string _username;
        private bool _isAuthenticating;

        public LoginViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value)
                    return;

                _username = value;
                NotifyOfPropertyChange(() => Username);
                Validate();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                NotifyOfPropertyChange(() => Password);
                Validate();
            }
        }

        public bool IsAuthenticating
        {
            get { return _isAuthenticating; }
            set
            {
                if (_isAuthenticating == value)
                    return;

                _isAuthenticating = value;
                NotifyOfPropertyChange(() => IsAuthenticating);
            }
        }

        public bool CanLogin
        {
            get { return _canLogin; }
            set
            {
                if (_canLogin == value)
                    return;

                _canLogin = value;
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public async void Login()
        {
            IsAuthenticating = true;
            try
            {
                var response = await _mediator.Send(new TokenRequest(Username, Password));
                if (string.IsNullOrEmpty(response.Token))
                {
                    Password = Username = null;
                    // TODO: redirect to server list (store token)
                }
            }
            catch (Exception ex)
            {
                // TODO: log exception
                // TODO: show validation msg in UI
            }
            finally
            {
                IsAuthenticating = false;
            }
        }

        private void Validate()
        {
            CanLogin = !string.IsNullOrEmpty(Username) & !string.IsNullOrEmpty(Password);
        }
    }
}
