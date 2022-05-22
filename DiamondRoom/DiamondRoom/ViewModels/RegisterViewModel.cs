using DiamondRoom.Commands;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _username;
        private string _password;
        private string _country;
        private string _phoneNumber1;
        private string _phoneNumber2;
        private string _city;
        private string _street;
        private string _errorMessage;

        public ICommand BackCommand { get; }
        public ICommand RegisterCommand { get; }
        public RegisterViewModel(NavigationStore navigationStore)
        {
            RegisterCommand = new RegisterCommand(this, navigationStore);
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, null));
        }
        
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string PhoneNumber1
        {
            get { return _phoneNumber1; }
            set { _phoneNumber1 = value; }
        }
        public string PhoneNumber2
        {
            get { return _phoneNumber2; }
            set { _phoneNumber2 = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
    }
}
