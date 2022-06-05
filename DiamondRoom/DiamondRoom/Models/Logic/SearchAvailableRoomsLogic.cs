using DiamondRoom.Commands;
using DiamondRoom.Stores;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.Logic
{
    public class SearchAvailableRoomsLogic
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private Tuple<DateTime?, DateTime?, string> _userSpecifications;

        public SearchAvailableRoomsLogic(NavigationStore navigationStore, User userLoggedIn, Tuple<DateTime?, DateTime?, string> userSpecifications)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            _userSpecifications = userSpecifications;

        }

        public void ReadMoreAboutClickedRoom(object obj)
        {
            SearchRoomCustom request = obj as SearchRoomCustom;
            if(request == null)
            {
                MessageBox.Show("Select an available room from the list!");
            }
            else
            {
                new NavigateCommand<SelectedRoomPossibleReservationViewModel>
                    (navigationStore,
                    () => new SelectedRoomPossibleReservationViewModel(navigationStore, userLoggedIn, _userSpecifications, request)).Execute(this);
            }
          
        }
    }
}
