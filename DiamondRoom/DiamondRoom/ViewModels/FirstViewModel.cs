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
using System.Windows;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        private User _userLoggedIn;
        private string _username;
        private DateTime? _dateFrom;
        private DateTime? _dateTo;
        private bool _showSignUp;
        private bool _showSignIn;
        private bool _showAdminMenu;
        private bool _showLogOut;
        private bool _showReservations;
        private bool _showHistory;
        private string _typedSearchInformations;

        private NavigationStore _navigationStore;

        private OfferBusinessLogic offerBusinessLogic = new OfferBusinessLogic();
        private AdvertismentBusinessLogic advertismentBusinessLogic;
        private DatePickerLogic datePickerLogic;


        public ICommand AdminMenuCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ReservationsCommand { get; }
        public FirstViewModel(NavigationStore navigationStore, User userLoggedIn)
        {
            _navigationStore = navigationStore;
            if (userLoggedIn == null)
            {
                _username = "Guest";
                _showSignIn = true;
                LoginCommand = new NavigateCommand<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationStore));

                _showSignUp = true;
                SignUpCommand = new NavigateCommand<RegisterViewModel>(navigationStore, () => new RegisterViewModel(navigationStore));
            }
            else
            {
                _showHistory = true;
                _userLoggedIn = userLoggedIn;
                LogOutCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, null));
                _username = "Hello, " + _userLoggedIn.firstName + " " + _userLoggedIn.lastName;
                _showLogOut = true;

                if (_userLoggedIn.accessLevel == 0) // 0 = admin
                {
                    _showAdminMenu = true;
                    AdminMenuCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(navigationStore, _userLoggedIn));
                }
                else if (_userLoggedIn.accessLevel == 1) // 1 = angajat
                {
                    _showReservations = true;
                    ReservationsCommand = new NavigateCommand<ReservationsRoomViewModel>(navigationStore, () => new ReservationsRoomViewModel(navigationStore, _userLoggedIn));
                }
            }
            advertismentBusinessLogic = new AdvertismentBusinessLogic(_navigationStore, _userLoggedIn);
            Advertisements = offerBusinessLogic.GetAdvertisments();

        }

        #region Command Members
        public void SearchRoomsButton(object obj)
        {
            datePickerLogic.SearchFreeRooms(obj);
        }

        private ICommand searchRoomsCommand;
        public ICommand SearchRoomsCommand
        {
            get
            {
                if (searchRoomsCommand == null)
                {
                    searchRoomsCommand = new RelayCommand(SearchRoomsButton);
                }
                return searchRoomsCommand;
            }
        }
        public void SeeOfferMethod(object obj)
        {
            advertismentBusinessLogic.NavigateToDetailedOffer(obj);
        }
        public void SeeHistoryReservations(object obj)
        {
            new NavigateCommand<HistoryReservationsViewModel>(_navigationStore, () => new HistoryReservationsViewModel(_navigationStore, _userLoggedIn)).Execute(this);
        }
        private ICommand historyReservations;
        public ICommand HistoryReservations
        {
            get
            {
                if (historyReservations == null)
                {
                    historyReservations = new RelayCommand(SeeHistoryReservations);
                }
                return historyReservations;
            }
        }

        private ICommand seeOfferCommand;
        public ICommand SeeOfferCommand
        {
            get
            {
                if (seeOfferCommand == null)
                {
                    seeOfferCommand = new RelayCommand(SeeOfferMethod);
                }
                return seeOfferCommand;
            }
        }
        #endregion
        public bool ShowHistory
        {
            get { return _showHistory; }
            set { _showHistory = value; }
        }

        public string TypedSearchInformations
        {
            get { return _typedSearchInformations; }
            set { _typedSearchInformations = value; }
        }
        public bool ShowReservations
        {
            get { return _showReservations; }
            set { _showReservations = value; }
        }

        public DateTime? DateFrom
        {
            get
            {
                //datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, _dateFrom, _dateTo, _typedSearchInformations);
                datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, new Tuple<DateTime?, DateTime?, string>(_dateFrom, _dateTo, _typedSearchInformations));
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                //datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, _dateFrom, _dateTo, _typedSearchInformations);
                datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, new Tuple<DateTime?, DateTime?, string>(_dateFrom, _dateTo, _typedSearchInformations));
            }
        }

        public DateTime? DateTo
        {
            get
            {
                //datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, _dateFrom, _dateTo, _typedSearchInformations);
                datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, new Tuple<DateTime?, DateTime?, string>(_dateFrom, _dateTo, _typedSearchInformations));
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                //datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, _dateFrom, _dateTo, _typedSearchInformations);
                datePickerLogic = new DatePickerLogic(_navigationStore, _userLoggedIn, new Tuple<DateTime?, DateTime?, string>(_dateFrom, _dateTo, _typedSearchInformations));
            }
        }
        public ObservableCollection<Advertisement> Advertisements
        {
            get => offerBusinessLogic.Advertisements;
            set => offerBusinessLogic.Advertisements = value;
        }

        public bool ShowLogOut
        {
            get { return _showLogOut; }
            set { _showLogOut = value; }
        }
        public bool ShowAdminMenu
        {
            get { return _showAdminMenu; }
            set { _showAdminMenu = value; }
        }
        public bool ShowSignUp
        {
            get { return _showSignUp; }
            set { _showSignUp = value; }
        }
        public bool ShowSignIn
        {
            get { return _showSignIn; }
            set { _showSignIn = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}