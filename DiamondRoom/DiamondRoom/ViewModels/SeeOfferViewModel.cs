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
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Advertisement> SelectedOffer
        {
            get => _advertismentBusinessLogic.Advertisements;
            set => _advertismentBusinessLogic.Advertisements = value;
        }

    }
}
