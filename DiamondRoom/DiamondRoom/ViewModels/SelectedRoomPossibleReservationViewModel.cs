using DiamondRoom.Commands;
using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class SelectedRoomPossibleReservationViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private Tuple<DateTime?, DateTime?, string> userSpecifications;
        private SearchRoomCustom request;
        private RoomBusinessLogic roomBusinessLogic = new RoomBusinessLogic();
        private ExtraFeatureBusinessLogic extraFeatureBusinessLogic = new ExtraFeatureBusinessLogic();

        public ICommand BackCommand { get; }
        private ICommand printList;

        private string _aditionalCost;
        public SelectedRoomPossibleReservationViewModel(NavigationStore navigationStore, User userLoggedIn, Tuple<DateTime?, DateTime?, string> userSpecifications, SearchRoomCustom request)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.userSpecifications = userSpecifications;
            this.request = request;
            BackCommand = new NavigateCommand<SearchRoomsViewModel>(navigationStore, () => new SearchRoomsViewModel(navigationStore, userLoggedIn, userSpecifications));
            SelectedRoom = roomBusinessLogic.GetSelectedPossibleReservation(request);
            ExtraFeaturesAvailable = extraFeatureBusinessLogic.GetExtraFeatures4List();

        }
        public void CheckExtraPrice(object obj)
        {
            for (int i = 0; i < ExtraFeaturesAvailable.Count; i++)
            {
                Console.WriteLine(ExtraFeaturesAvailable[i].name + " -> " + ExtraFeaturesAvailable[i].isChecked);
            }
        }

        public string AditionalCost
        {
           get => _aditionalCost;
           set => _aditionalCost = value;
        }
        public ICommand PrintList
        {
            get
            {
                if (printList == null)
                {
                    printList = new RelayCommand(CheckExtraPrice);
                }
                return printList;
            }
        }

        public ObservableCollection<SearchRoomCustom> SelectedRoom
        {
            get => roomBusinessLogic.SelectedRoom;
            set => roomBusinessLogic.SelectedRoom = value;
        }

        public ObservableCollection<ExtraFeatureCustom> ExtraFeaturesAvailable
        {
            get
            {
                _aditionalCost = extraFeatureBusinessLogic.GetAditionalCostForCheckedFeatures(extraFeatureBusinessLogic.ExtraFeaturesCustom4ComboList).ToString();
                OnPropertyChanged(nameof(AditionalCost));
                return extraFeatureBusinessLogic.ExtraFeaturesCustom4ComboList;
            }
            set => extraFeatureBusinessLogic.ExtraFeaturesCustom4ComboList = value;
        }



    }
}
