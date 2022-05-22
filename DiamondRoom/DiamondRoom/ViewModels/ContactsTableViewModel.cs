using DiamondRoom.Commands;
using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiamondRoom.ViewModels
{
    public class ContactsTableViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private User _admin;
        private NavigationStore _navigationStore;
        private ContactBusinessLogic contactBusinessLogic = new ContactBusinessLogic();

        public ContactsTableViewModel(NavigationStore navigationStore, User admin)
        {
            _navigationStore = navigationStore;
            _admin = admin;
            Contacts = contactBusinessLogic.GetAllContacts();
            BackCommand = new NavigateCommand<AdminPanelViewModel>(navigationStore, () => new AdminPanelViewModel(_navigationStore, _admin));
            
        }


        public ICommand UpdateContactsCommand { get; }
        public ICommand BackCommand { get; }
        public ObservableCollection<Contact> Contacts
        {
            get => contactBusinessLogic.Contacts;
            set => contactBusinessLogic.Contacts = value;
        }

        #region Command Members
        //am adus aici metodele din BLL ca sa pot actualiza ErrorMesage
        public void AddMethod(object obj)
        {
            contactBusinessLogic.AddMethod(obj);
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
            contactBusinessLogic.UpdateMethod(obj);
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
            contactBusinessLogic.DeleteMethod(obj);
           
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
