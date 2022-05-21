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
        private string _username;
        private bool _userLoggedIn;
        private bool _showSignUp;
        private bool _showSignIn;
        private bool _showLogOut; 

        private ContactBusinessLogic contactBL;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public FirstViewModel(NavigationStore navigationStore, bool userLoggedIn)
        {
            _userLoggedIn = userLoggedIn;
            if (!_userLoggedIn)
            {
                _username = "Guest";

                _showSignIn = true;
                LoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));

                _showSignUp = true;
                SignUpCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
            }
            else
            {
                _showLogOut = true;
            }

            contactBL = new ContactBusinessLogic();
            //contactBL.AddMethod(this);
            ContactList = contactBL.GetAllContacts();
        }

        public ObservableCollection<Contact> ContactList
        {
            get => contactBL.ContactList;
            set => contactBL.ContactList = value;
        }
     
        public bool ShowLogOut
        {
            get { return _showLogOut; }
            set { _showLogOut = value; }
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
