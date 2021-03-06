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
        public ICommand FeaturesTableCommand { get; }
        public ICommand ExtraFeaturesTableCommand { get; }
        public ICommand RoomsTableCommand { get; }
        public ICommand RoomTypesCommand { get; }
        public ICommand OffersCommand { get; }
        public ICommand ActiveReservations { get; }
        public AdminPanelViewModel(NavigationStore navigationStore, User admin)
        {
            _admin = admin;
            _navigationStore = navigationStore;
            FeaturesTableCommand = new NavigateCommand<FeaturesTableViewModel>(_navigationStore, () => new FeaturesTableViewModel(_navigationStore, _admin));
            ContactsTableCommand = new NavigateCommand<ContactsTableViewModel>(_navigationStore, () => new ContactsTableViewModel(_navigationStore, _admin));
            BackCommand = new NavigateCommand<FirstViewModel>(_navigationStore, () => new FirstViewModel(_navigationStore, _admin));
            ExtraFeaturesTableCommand = new NavigateCommand<ExtraFeatureTableViewModel>(_navigationStore, () => new ExtraFeatureTableViewModel(_navigationStore, _admin));
            RoomsTableCommand = new NavigateCommand<RoomsTableViewModel>(_navigationStore, () => new RoomsTableViewModel(_navigationStore, _admin));
            RoomTypesCommand = new NavigateCommand<RoomTypeViewModel>(_navigationStore, () => new RoomTypeViewModel(_navigationStore, _admin));
            OffersCommand = new NavigateCommand<OffersViewModel>(_navigationStore, () => new OffersViewModel(_navigationStore, _admin));
            ActiveReservations = new NavigateCommand<ReservationsRoomViewModel>(_navigationStore, () => new ReservationsRoomViewModel(_navigationStore, _admin));
        }
    }
}
