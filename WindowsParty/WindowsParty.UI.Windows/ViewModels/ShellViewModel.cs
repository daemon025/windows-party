using Caliburn.Micro;

namespace WindowsParty.UI.Windows.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly LoginViewModel _loginViewModel;

        public ShellViewModel(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(_loginViewModel);
        }
    }
}