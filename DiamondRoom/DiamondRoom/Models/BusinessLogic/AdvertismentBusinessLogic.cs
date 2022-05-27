using DiamondRoom.Commands;
using DiamondRoom.Stores;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class AdvertismentBusinessLogic
    {
        private NavigationStore _navigationStore;
        private User _userLoggedIn;
        private OfferBusinessLogic _offerBusinessLogic = new OfferBusinessLogic();
        public ObservableCollection<Advertisement> Advertisements { get; set; }

        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Advertisement> Advertisments { get; set; }

        public AdvertismentBusinessLogic(NavigationStore navigationStore, User user)
        {
            _navigationStore = navigationStore;
            _userLoggedIn = user;
            Advertisements = new ObservableCollection<Advertisement>();
        }
        public void NavigateToDetailedOffer(object obj)
        {
            Advertisement advertisment = obj as Advertisement;

            if (advertisment != null)
            {
                new NavigateCommand<SeeOfferViewModel>(_navigationStore, () => new SeeOfferViewModel(_navigationStore, _userLoggedIn, advertisment)).Execute(this);
            }
            else
            {
                MessageBox.Show("Selecteaza o oferta din lista!");
            }
        }

        public ObservableCollection<Advertisement> GetSelectedOffer(int roomId)
        {
            List<Advertisement> advertisements = _offerBusinessLogic.GetAdvertisments().ToList();
            ObservableCollection<Advertisement> result = new ObservableCollection<Advertisement>();
            foreach(Advertisement advert in advertisements)
            {
                if(advert.idRoom == roomId)
                {
                    result.Add(advert);
                    return result;
                }
            }
            return null;
        }

        //functie ce returneaza oferta selectata ca o lista de observable collectiooin de size 1 (cea selectata)

    }
}
