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
        private string _user;
        private string _password;
        private UserBusinessLogic userBussinessLogic = new UserBusinessLogic();
        public LoginCommand(NavigationStore navigationStore, string user, string password)
        {
            _user = user;
            _password = password;
            Console.WriteLine("CMD: "+_user+"\n"+_password);
            _navigationStore = navigationStore;
        }
        public override void Execute(object parameter)
        {
            User response = userBussinessLogic.CheckIfUserExists(_user, _password);
            if (response != null)
            {
                new NavigateCommand<FirstViewModel>(_navigationStore, () => new FirstViewModel(_navigationStore, response)).Execute(this);
                Console.WriteLine("Successfully logged in");
            }
            else
            {
                MessageBox.Show("Username or password incorrect!");
            }
        }
    }
}
