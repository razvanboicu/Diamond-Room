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
    public class SeeOfferViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private User _userLoggedIn;
        private AdvertismentBusinessLogic _advertismentBusinessLogic;
        private RoomBusinessLogic roomBusinessLogic = new RoomBusinessLogic();
        private RoomFeaturesBusinessLogic roomFeatureBusinessLogic = new RoomFeaturesBusinessLogic();
        private string _oldPrice;
        private string _newPrice;
        private string _youSave;

        private Advertisement _advertisement;
        private ObservableCollection<Advertisement> _advertisments { get; set; }
        public SeeOfferViewModel(NavigationStore navigationStore, User userLoggedIn, Advertisement obj)
        {
            _navigationStore = navigationStore;
            _userLoggedIn = userLoggedIn;
            _advertisement = obj;
            _advertisments = new ObservableCollection<Advertisement>();
            _advertismentBusinessLogic = new AdvertismentBusinessLogic(_navigationStore, _userLoggedIn);

            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
            SelectedOffer = _advertismentBusinessLogic.GetSelectedOffer(_advertisement.idRoom);
            _oldPrice = "[OLD PRICE] " + roomBusinessLogic.GetPriceForSpecifiecIdRoom(_advertisement.idRoom).ToString() + " lei";
            _newPrice = "[NEW PRICE] " + (int) roomBusinessLogic.GetDiscountedPriceWParameters(roomBusinessLogic.GetPriceForSpecifiecIdRoom(_advertisement.idRoom), _advertisement.discount) + " lei";
            _youSave = " -" + ((int)roomBusinessLogic.GetPriceForSpecifiecIdRoom(_advertisement.idRoom) - (int)roomBusinessLogic.GetDiscountedPriceWParameters(roomBusinessLogic.GetPriceForSpecifiecIdRoom(_advertisement.idRoom), _advertisement.discount)).ToString()+ " lei";

            DefaultFeatures = roomFeatureBusinessLogic.GetFeaturesForSpecifiedRoomID(_advertisement.idRoom);
        }
        public string YouSave
        {
            get { return _youSave; }
            set { _youSave = value; }
        }
        public string NewPrice
        {
            get { return _newPrice; }
            set { _newPrice = value; }
        }

        public string OldPrice
        {
            get { return _oldPrice; }
            set { _oldPrice = value; }
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Feature> DefaultFeatures
        {
            get => roomFeatureBusinessLogic.FeaturesForSpecifiedRoom;
            set => roomFeatureBusinessLogic.FeaturesForSpecifiedRoom = value;
        }
        public ObservableCollection<Advertisement> SelectedOffer
        {
            get => _advertismentBusinessLogic.Advertisements;
            set => _advertismentBusinessLogic.Advertisements = value;
        }
        // [OK] avem pretul initial, apoi in dreapta o sa avem noul pret cu rosu asa frumos ca la oferta 
        //avem extraoptiuni default, doar le afisam, si apoi avem extraoptiune cu bani pe care le putem adauga la rezervarea noastra
        // voucher ca pe tazz
    }
}
