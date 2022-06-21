using DiamondRoom.Commands;
using DiamondRoom.Models;
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
    public class ProceedReservationViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private SearchRoomCustom request;
        private ObservableCollection<ExtraFeatureCustom> extraFeaturesAvailable;
        private Tuple<DateTime?, DateTime?, string> _userSpecifications;

        public ICommand BackCommand { get; }

        public ProceedReservationViewModel(NavigationStore navigationStore, User userLoggedIn, 
            SearchRoomCustom request, ObservableCollection<ExtraFeatureCustom> extraFeaturesAvailable, Tuple<DateTime?, DateTime?, string> userSpecifications)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.request = request;
            this.extraFeaturesAvailable = extraFeaturesAvailable;
            _userSpecifications = userSpecifications;
            BackCommand = new NavigateCommand<SelectedRoomPossibleReservationViewModel>(navigationStore, () => 
                            new SelectedRoomPossibleReservationViewModel(navigationStore, userLoggedIn, userSpecifications, request));

        }
    }
}
