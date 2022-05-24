using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models.BusinessLogic
{
    public class OfferBusinessLogic
    {
        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<OfferCustom> OffersCustom { get; set; }
        public ObservableCollection<Advertisement> Advertisements { get; set; }
        public OfferBusinessLogic()
        {
            Offers = new ObservableCollection<Offer>();
            Advertisements = new ObservableCollection<Advertisement>();
            OffersCustom = new ObservableCollection<OfferCustom>();
        }

        public ObservableCollection<Advertisement> GetAdvertisments()
        {
            ObservableCollection<Advertisement> result = new ObservableCollection<Advertisement>();

            List<Room> rooms = context.Rooms.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();
            List<Offer> offers = context.Offers.ToList();

            foreach(Room room in rooms)
            {
                int idR = room.id;
                foreach(Offer offer in offers)
                {
                    if(offer.fk_room == idR)
                    {
                        foreach(Room_type room_Type in roomTypes)
                        {
                            if(room_Type.id == room.fk_type)
                            {
                                result.Add(new Advertisement()
                                {
                                    idRoom = room.id,
                                    discount = offer.discount,
                                    roomType = room_Type.type,
                                    info = offer.info
                                });
                            }
                        }
                        //break; //daca vrem sa ne ia doar prima oferta per camera din oferte
                    }
                }
            }
            return result;
        }
        public ObservableCollection<Offer> GetAllOffers()
        {
            List<Offer> offers = context.Offers.ToList();
            ObservableCollection<Offer> result = new ObservableCollection<Offer>();
            foreach (Offer roomType in offers)
            {
                result.Add(roomType);
            }
            return result;
        }

        public void AddOfferCustom(int fkRoom, int disc, string inf, bool del)
        {
            Offer newOffer = new Offer()
            {
                fk_room = fkRoom,
                discount = disc,
                info = inf,
                deleted = del
            };

            context.Offers.Add(newOffer);
            context.SaveChanges();
            newOffer.id = context.Offers.Max(item => item.id);
            Offers.Add(newOffer);
            Console.WriteLine("New offers added to the database");

        }
        public ObservableCollection<OfferCustom> GetAllCustomOffers()
        {
            List<Offer> offers = context.Offers.ToList();
            List<Room> rooms = context.Rooms.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();

            ObservableCollection<OfferCustom> result = new ObservableCollection<OfferCustom>();

            foreach (Offer offer in offers)
            {
                foreach (Room room in rooms)
                {
                    if (room.id == offer.fk_room)
                    {
                        foreach (Room_type roomType in roomTypes)
                        {
                            if (room.fk_type == roomType.id)
                            {
                                result.Add(new OfferCustom()
                                {
                                    id = room.id,
                                    type = roomType.type,
                                    price = (float)roomType.price,
                                    discount = offer.discount,
                                    available = offer.deleted,
                                    obs = offer.info
                                });
                            }

                        }

                    }
                }
            }
            return result;
        }
    }
}
