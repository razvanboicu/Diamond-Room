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
    public class ReservationsRoomViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private User _userLoggedIn;

        private ReservationBusinessLogic reservationBusinessLogic = new ReservationBusinessLogic(); 

        public ICommand BackCommand { get; }
        public ReservationsRoomViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _userLoggedIn = admin;
            if(_userLoggedIn.accessLevel == 0)
                BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _userLoggedIn));
            else BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(_navigationStore, _userLoggedIn));
            Reservations = reservationBusinessLogic.GetAllCustomReservations();
        }
        
        public ObservableCollection<ReservationCustom> Reservations
        {
            get => reservationBusinessLogic.ReservationsCustom;
            set => reservationBusinessLogic.ReservationsCustom = value;
        }

        public void UpdateMethod(object obj)
        {
            reservationBusinessLogic.ModifyStatusReservation(obj);
        }
        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(UpdateMethod);
                }
                return updateCommand;
            }
        }

    }
}
