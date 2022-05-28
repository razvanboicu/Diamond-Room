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
    public class SearchRoomsViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private DateTime? dateFrom;
        private DateTime? dateTo;

        public ICommand BackCommand { get; }

        public SearchRoomsViewModel(NavigationStore navigationStore, User userLoggedIn, DateTime? dateFrom, DateTime? dateTo)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;

            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
        }
    }
}
