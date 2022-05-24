using DiamondRoom.Commands;
using DiamondRoom.Models;
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
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private Advertisement _advertisement;
        private ObservableCollection<Advertisement> _advertisments { get; set; }
        public SeeOfferViewModel(NavigationStore navigationStore, User userLoggedIn, object obj)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            _advertisement = obj as Advertisement;
            SelectedOffer = GetCustomCollection();
            _advertisments = new ObservableCollection<Advertisement>();
            BackCommand = new NavigateCommand<FirstViewModel>(navigationStore, () => new FirstViewModel(navigationStore, userLoggedIn));
            //Console.WriteLine("Test:" + (obj as Advertisement).roomType + "" +
            //    "\n" + (obj as Advertisement).discount);
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Advertisement> SelectedOffer
        {
            get => _advertisments;
            set => _advertisments = value;
        }

        private ObservableCollection<Advertisement> GetCustomCollection()
        {
            ObservableCollection<Advertisement> result = new ObservableCollection<Advertisement>();
            result.Add(_advertisement);
            return result;
        }
    }
}
