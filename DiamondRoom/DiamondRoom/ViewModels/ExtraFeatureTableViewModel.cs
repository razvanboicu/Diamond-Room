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
    public class ExtraFeatureTableViewModel : ViewModelBase 
    {
        private User _admin;
        private NavigationStore _navigationStore;
        private ExtraFeatureBusinessLogic extraFeatureBusinessLogic = new ExtraFeatureBusinessLogic();

        public ExtraFeatureTableViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _admin = admin;
            BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _admin));
            ExtraFeatures = extraFeatureBusinessLogic.GetAllExtraFeatures();
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Extra_features> ExtraFeatures
        {
            get => extraFeatureBusinessLogic.ExtraFeatures;
            set => extraFeatureBusinessLogic.ExtraFeatures = value;
        }

        #region Command Members
        //am adus aici metodele din BLL ca sa pot actualiza ErrorMesage
        public void AddMethod(object obj)
        {
            extraFeatureBusinessLogic.AddExtraFeature(obj);
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
            extraFeatureBusinessLogic.ModifyFeature(obj);
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
            extraFeatureBusinessLogic.DeleteMethod(obj);
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
