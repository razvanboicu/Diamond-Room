using DiamondRoom.Commands;
using DiamondRoom.Models;
using DiamondRoom.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class AdminPanelViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private User _admin;

        public ICommand ContactsTableCommand { get; }
        public ICommand BackCommand { get; }
        public AdminPanelViewModel(NavigationStore navigationStore, User admin)
        {
            _admin = admin;
            _navigationStore = navigationStore;
            ContactsTableCommand = new NavigateCommand<ContactsTableViewModel>(_navigationStore, () => new ContactsTableViewModel(_navigationStore, _admin));
            BackCommand = new NavigateCommand<FirstViewModel>(_navigationStore, () => new FirstViewModel(_navigationStore, _admin));
        }
    }
}
