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
    public class HistoryReservationsViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private ReservationBusinessLogic reservationBusinessLogic = new ReservationBusinessLogic();
        public HistoryReservationsViewModel(NavigationStore navigationStore, User userLoggedIn)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
            Reservations = reservationBusinessLogic.GetAllCustomReservationsForSpecificUserId(userLoggedIn.id);
        }

        public ICommand BackCommand { get; }

        public ObservableCollection<ReservationCustom> Reservations
        {
            get => reservationBusinessLogic.ReservationsCustom;
            set => reservationBusinessLogic.ReservationsCustom = value;
        }
    }
}
