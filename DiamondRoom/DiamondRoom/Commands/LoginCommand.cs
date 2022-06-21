using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Commands
{
    public class LoginCommand : CommandBase
    {
        NavigationStore _navigationStore;
        private LoginViewModel _loginViewModel;
        private UserBusinessLogic userBussinessLogic = new UserBusinessLogic();
        public LoginCommand(LoginViewModel loginViewModel, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _loginViewModel = loginViewModel;
        }
        public override void Execute(object parameter)
        {
            Console.WriteLine("User typed" +"\nuser:"+_loginViewModel.Username + "\npass:" + _loginViewModel.Password);
            User response = userBussinessLogic.CheckIfUserExists(_loginViewModel.Username, _loginViewModel.Password);
            if (response != null)
            {
                new NavigateCommand<FirstViewModel>(_navigationStore, () => new FirstViewModel(_navigationStore, response)).Execute(this);
                Console.WriteLine("Successfully logged in\n");
            }
            else
            {
                Console.WriteLine("Unsuccessfully attempt for login\n");
                MessageBox.Show("Username or password incorrect!");
            }
        }
    }
}
