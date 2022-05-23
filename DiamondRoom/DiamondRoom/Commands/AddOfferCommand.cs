using DiamondRoom.Models;
using DiamondRoom.Models.BusinessLogic;
using DiamondRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Commands
{
    public class AddOfferCommand : CommandBase
    {
        private RoomTypeBusinessLogic roomTypeBusinessLogic = new RoomTypeBusinessLogic();
        private RoomBusinessLogic roomBusinessLogic = new RoomBusinessLogic();
        private OfferBusinessLogic offerBusinessLogic = new OfferBusinessLogic();
        private OffersViewModel _offersViewModel;

        public AddOfferCommand(OffersViewModel offersViewModel)
        {
            _offersViewModel = offersViewModel;
        }
        public override void Execute(object parameter)
        {
            AddOffer(parameter);
        }

        private void AddOffer(object obj)
        {
            OfferCustom offerCustom = obj as OfferCustom;
            int fkType = roomTypeBusinessLogic.GetIdForSpecifiedType(offerCustom.type);
            if (fkType != -69 && roomBusinessLogic.CheckIfAnIdExists(offerCustom.id))
            {
                offerBusinessLogic.AddOfferCustom(offerCustom.id, offerCustom.discount, offerCustom.obs, offerCustom.available);
            }
            else
            {
                MessageBox.Show("Incorect data");
            }
        }
    }
}
