using DiamondRoom.Commands;
using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Models.Logic;
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
    public class SearchRoomsViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private RoomBusinessLogic roomBusinessLogic = new RoomBusinessLogic();

        public ICommand BackCommand { get; }
        private ICommand readMoreCommand;
        public SearchRoomsViewModel(NavigationStore navigationStore, User userLoggedIn, DateTime? dateFrom, DateTime? dateTo)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            Console.WriteLine(dateFrom + "\n And To:" + dateTo);
            
            AvailableRooms = roomBusinessLogic.GetRoomsAvailableSelectedPeriodOfTime(dateFrom, dateTo);
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
        }
        public ObservableCollection<SearchRoomCustom> AvailableRooms
        {
            get => roomBusinessLogic.RoomsSearched;
            set => roomBusinessLogic.RoomsSearched = value;
        }
        public void SearchAvailableRoomsButton(object obj)
        {
            SearchAvailableRoomsLogic.SearchAvailableRooms(obj);
        }
        public ICommand ReadMoreCommand
        {
            get
            {
                if (readMoreCommand == null)
                {
                    readMoreCommand = new RelayCommand(SearchAvailableRoomsButton);
                }
                return readMoreCommand;
            }
        }
       
    }
}
