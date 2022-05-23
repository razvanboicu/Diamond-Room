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
    public class OffersViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private User _admin;
        private OfferBusinessLogic offerBusinessLogic = new OfferBusinessLogic();
        public ICommand BackCommand { get; }
        public OffersViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _admin = admin;
            BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _admin));
            OffersCustom = offerBusinessLogic.GetAllCustomOffers();
            AddCommand = new AddOfferCommand(this);
        }

        
        public ObservableCollection<OfferCustom> OffersCustom
        {
            get => offerBusinessLogic.OffersCustom;
            set => offerBusinessLogic.OffersCustom = value;
        }

        #region Command Members

        private ICommand addCommand;
        public ICommand AddCommand { get; }
        public void UpdateMethod(object obj)
        {
            //featureBusinessLogic.ModifyFeature(obj);
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

        public void DeleteMethod(object obj)
        {
           // featureBusinessLogic.DeleteMethod(obj);
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(DeleteMethod);
                }
                return deleteCommand;
            }
        }

        #endregion
    }
}
