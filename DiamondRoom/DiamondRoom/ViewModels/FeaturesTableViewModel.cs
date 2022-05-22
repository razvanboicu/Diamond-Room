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
    public class FeaturesTableViewModel : ViewModelBase
    {
        private User _admin;
        private NavigationStore _navigationStore;
        private FeatureBusinessLogic featureBusinessLogic = new FeatureBusinessLogic();

        public FeaturesTableViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _admin = admin;
            BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _admin));
            Featuress = featureBusinessLogic.GetAllFeatures();
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Feature> Featuress
        {
            get => featureBusinessLogic.Features;
            set => featureBusinessLogic.Features = value;
        }

        #region Command Members
        //am adus aici metodele din BLL ca sa pot actualiza ErrorMesage
        public void AddMethod(object obj)
        {
            featureBusinessLogic.AddFeature(obj);
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddMethod);
                }
                return addCommand;
            }
        }

        public void UpdateMethod(object obj)
        {
            featureBusinessLogic.ModifyFeature(obj);
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
            featureBusinessLogic.DeleteMethod(obj);
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
