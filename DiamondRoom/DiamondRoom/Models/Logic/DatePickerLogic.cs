using DiamondRoom.Commands;
using DiamondRoom.Stores;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.Logic
{
    public class DatePickerLogic
    {
        private NavigationStore navigationStore;
        private User userLoggedIn;
        private DateTime? dateFrom;
        private DateTime? dateTo;
        private string typedSearchInfo;
        

        public DatePickerLogic(NavigationStore navigationStore, User userLoggedIn, DateTime? dateFrom, DateTime? dateTo, string typedInfo)
        {
            this.navigationStore = navigationStore;
            this.userLoggedIn = userLoggedIn;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
            typedSearchInfo = typedInfo;
        }

        public void SearchFreeRooms(object obj)
        {
            if(dateFrom.HasValue == true && dateTo.HasValue == true)
            {
                if(dateFrom < dateTo)
                {
                    Console.WriteLine("Typed Info: " + typedSearchInfo);
                    new NavigateCommand<SearchRoomsViewModel>(navigationStore, () => new SearchRoomsViewModel(navigationStore, userLoggedIn, dateFrom, dateTo)).Execute(this);
                }
                else
                {
                    MessageBox.Show("[Date from] should be less than [Date to]!");
                }
            }
            else
            {
                MessageBox.Show("Select the period for your reservation");
            }
        }
    }
}
