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
        private string userTypedInfo;
        private RoomBusinessLogic roomBusinessLogic = new RoomBusinessLogic();
        private SearchAvailableRoomsLogic searchAvailableRoomsLogic;

        public ICommand BackCommand { get; }
        private ICommand readMoreCommand;
        public SearchRoomsViewModel(NavigationStore navigationStore, User userLoggedIn, Tuple<DateTime?, DateTime?, string> userSpecifications)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.dateFrom = userSpecifications.Item1;
            this.dateTo = userSpecifications.Item2;
            userTypedInfo = userSpecifications.Item3;

            Console.WriteLine(dateFrom + "\n And To:" + dateTo);
            
            AvailableRooms = roomBusinessLogic.GetRoomsAvailableSelectedPeriodOfTime(dateFrom, dateTo);
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
            searchAvailableRoomsLogic = new SearchAvailableRoomsLogic(this.navigationStore, this.userLoggedIn, new Tuple<DateTime?, DateTime?, string>(dateFrom, dateTo, userTypedInfo));
        }
        public ObservableCollection<SearchRoomCustom> AvailableRooms
        {
            get => roomBusinessLogic.RoomsSearched;
            set => roomBusinessLogic.RoomsSearched = value;
        }
        public void ReadMoreAboutSelectedRoom(object obj)
        {
            searchAvailableRoomsLogic.ReadMoreAboutClickedRoom(obj);
        }
        public ICommand ReadMoreCommand
        {
            get
            {
                if (readMoreCommand == null)
                {
                    readMoreCommand = new RelayCommand(ReadMoreAboutSelectedRoom);
                }
                return readMoreCommand;
            }
        }
       
    }
}
