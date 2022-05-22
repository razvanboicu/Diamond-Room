using DiamondRoom.Commands;
using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        private User _userLoggedIn;
        private string _username;
        private bool _showSignUp;
        private bool _showSignIn;
        private bool _showAdminMenu;
        private bool _showLogOut;

        private ContactBusinessLogic contactBusinessLogic = new ContactBusinessLogic();

        public ICommand AdminMenuCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public FirstViewModel(NavigationStore navigationStore, User userLoggedIn)
        {
            if (userLoggedIn == null)
            {
                _username = "Guest";
                _showSignIn = true;
                LoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));

                _showSignUp = true;
                SignUpCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
            }
            else
            {
                _userLoggedIn = userLoggedIn;
                LogOutCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, null));
                _username = "Hello, " + _userLoggedIn.firstName + " " + _userLoggedIn.lastName;
                _showLogOut = true;

                if (_userLoggedIn.accessLevel == 0)
                {
                    _showAdminMenu = true;
                    AdminMenuCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(navigationStore, _userLoggedIn));
                }
            }
            ContactList = contactBusinessLogic.GetAllContacts();
        }

        public ObservableCollection<Contact> ContactList
        {
            get => contactBusinessLogic.Contacts;
            set => contactBusinessLogic.Contacts = value;
        }

        public bool ShowLogOut
        {
            get { return _showLogOut; }
            set { _showLogOut = value; }
        }
        public bool ShowAdminMenu
        {
            get { return _showAdminMenu; }
            set { _showAdminMenu = value; }
        }
        public bool ShowSignUp
        {
            get { return _showSignUp; }
            set { _showSignUp = value; }
        }
        public bool ShowSignIn
        {
            get { return _showSignIn; }
            set { _showSignIn = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}
