using DiamondRoom.Commands;
using DiamondRoom.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private string _username;
        private string _password;

        public LoginViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            LoginCommand = new LoginCommand(navigationStore, _username, _password);
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, null));
        }
        public ICommand LoginCommand { get; }
        public ICommand BackCommand{ get; }
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
    }
}
