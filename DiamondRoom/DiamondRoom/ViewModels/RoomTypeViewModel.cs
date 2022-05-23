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
    public class RoomTypeViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private User _admin;
        private RoomTypeBusinessLogic roomTypeBusinessLogic = new RoomTypeBusinessLogic();

        public RoomTypeViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _admin = admin;
            RoomTypes = roomTypeBusinessLogic.GetAllRoomTypes();
            BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _admin));
        }

        public ICommand BackCommand { get; }
        public ObservableCollection<Room_type> RoomTypes
        {
            get => roomTypeBusinessLogic.RoomTypes;
            set => roomTypeBusinessLogic.RoomTypes = value;
        }
        #region Command Members

        public void AddMethod(object obj)
        {
            roomTypeBusinessLogic.AddRoomType(obj);
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
            roomTypeBusinessLogic.ModifyRoomType(obj);
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
            roomTypeBusinessLogic.DeleteMethod(obj);
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
