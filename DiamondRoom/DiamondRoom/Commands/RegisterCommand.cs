using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Commands
{
    public class RegisterCommand : CommandBase
    {
        private RegisterViewModel _registerViewModel;
        private NavigationStore _navigationStore;
        private AddressBusinessLogic addressBusinessLogic = new AddressBusinessLogic();
        private ContactBusinessLogic contactBusinessLogic = new ContactBusinessLogic();
        private UserBusinessLogic userBusinessLogic = new UserBusinessLogic();  
        
        static public string errMsj;

        public RegisterCommand(RegisterViewModel registerViewModel, NavigationStore navigationStore)
        {
            _registerViewModel = registerViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            AddUser(parameter);

        }

        private void AddUser(object obj)
        {
            UserCustom userCustom = obj as UserCustom;
            int fkAddress = addressBusinessLogic.AddAddress(userCustom._country, userCustom._city, userCustom._street);
            int fkContact = contactBusinessLogic.AddContact(userCustom._email, userCustom._phoneNr1, userCustom._phoneNr2);
            if(fkAddress != -1 && fkContact != -1)
                 userBusinessLogic.AddUser(userCustom._username, userCustom._password, userCustom._firstName, userCustom._lastName, fkAddress, fkContact);
            new NavigateCommand<FirstViewModel>(_navigationStore, () => new FirstViewModel(_navigationStore, null)).Execute(this);
        }

        
    }
}
