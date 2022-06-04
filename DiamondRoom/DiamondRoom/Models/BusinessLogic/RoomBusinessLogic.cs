using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class RoomBusinessLogic
    {
        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<RoomCustom> RoomsCustom { get; set; }
        public ObservableCollection<SearchRoomCustom> RoomsSearched { get; set; }
        private RoomTypeBusinessLogic roomTypeBusinessLogic = new RoomTypeBusinessLogic();
        public RoomBusinessLogic()
        {
            Rooms = new ObservableCollection<Room>();
            RoomsCustom = new ObservableCollection<RoomCustom>();
            RoomsSearched = new ObservableCollection<SearchRoomCustom>();
        }

        public ObservableCollection<SearchRoomCustom> GetRoomsAvailableSelectedPeriodOfTime(DateTime? from, DateTime? to)
        {
            ObservableCollection<SearchRoomCustom> result = new ObservableCollection<SearchRoomCustom>();
            List<Room_reservations> roomReservations = context.Room_reservations.ToList();
            List<Reservation> reservations = context.Reservations.ToList();
            List<Room> rooms = context.Rooms.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();
            List<Offer> offers = context.Offers.ToList();
            HashSet<int> busyRooms = new HashSet<int>();

            foreach (Room_reservations roomReservation in roomReservations)
            {
                foreach (Reservation reservation in reservations)
                {
                    if (roomReservation.fk_reservation == reservation.id)
                    {
                        if (from >= reservation.dateFrom && from <= reservation.dateTo)
                        {
                            busyRooms.Add(roomReservation.fk_room);
                            break;
                        }
                        else if (from < reservation.dateFrom && to <= reservation.dateTo && to >= reservation.dateFrom)
                        {
                            busyRooms.Add(roomReservation.fk_room);
                            break;
                        }
                        else if (from >= reservation.dateFrom && from <= reservation.dateTo && to > reservation.dateTo)
                        {
                            busyRooms.Add(roomReservation.fk_room);
                            break;
                        }
                    }
                }
            }

            
            foreach (Room room in rooms)
            {
                if (!busyRooms.Contains(room.id))
                {
                    foreach (Room_type roomType in roomTypes)
                    {
                        SearchRoomCustom searchRoom = new SearchRoomCustom();
                        if (room.fk_type == roomType.id)
                        {
                            searchRoom._roomID = room.id;
                            searchRoom._roomType = roomType.type;
                            searchRoom._price = (int)roomType.price;

                            bool has = false;
                            foreach (Offer offer in offers)
                            {
                                if (offer.fk_room == room.id)
                                {
                                    has = true;
                                    break;
                                }
                            }
                            if (has) searchRoom._hasOffer = true;
                            else searchRoom._hasOffer = false;
                            result.Add(searchRoom);
                            break;
                            //trebuie sa creez un obiect de tipul RoomSearchCustom 
                        }
                    }
                }
            }
            foreach(var availableRooms in result)
            {
                Console.WriteLine(availableRooms._roomID + " " + availableRooms._roomType + " " + availableRooms._price + " " + availableRooms._hasOffer);
            }
            return result;
        }

        public bool CheckIfAnIdExists(int idSearched)
        {
            List<Room> rooms = context.Rooms.ToList();
            foreach (Room room in rooms)
            {
                if (room.id == idSearched)
                    return true;
            }
            return false;
        }
        public ObservableCollection<RoomCustom> GetAllRoomsCustom()
        {
            List<Room> rooms = context.Rooms.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();
            ObservableCollection<RoomCustom> result = new ObservableCollection<RoomCustom>();
            foreach (Room room in rooms)
            {
                foreach (Room_type roomType in roomTypes)
                {
                    if (roomType.id == room.fk_type)
                    {
                        result.Add(new RoomCustom()
                        {
                            id = room.id,
                            available = room.available,
                            type = roomType.type,
                            price = (int)roomType.price
                        });
                        break;
                    }
                }
            }
            return result;
        }

        public RoomCustom GetSpecifiedIdUserCustom(int idSearched)
        {
            List<Room> rooms = context.Rooms.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();

            foreach (Room room in rooms)
            {
                if (room.id == idSearched)
                {
                    foreach (Room_type roomType in roomTypes)
                    {
                        if (roomType.id == room.fk_type)
                        {
                            return new RoomCustom()
                            {
                                id = room.id,
                                available = room.available,
                                type = roomType.type,
                                price = (int)roomType.price
                            };
                        }
                    }
                }

            }
            return null;
        }
        public ObservableCollection<Room> GetAllRooms()
        {
            List<Room> rooms = context.Rooms.ToList();
            ObservableCollection<Room> result = new ObservableCollection<Room>();
            foreach (Room con in rooms)
            {
                result.Add(con);
            }
            return result;
        }

        public void AddRoom(object obj)
        {
            if (obj != null)
            {
                if ((obj as RoomCustom).available == null || (obj as RoomCustom).type == null)
                {
                    MessageBox.Show("Completeaza campurile available si type!");
                }
                else
                {
                    int fkForRoomType = roomTypeBusinessLogic.GetIdForSpecifiedType((obj as RoomCustom).type);
                    if (fkForRoomType != -1)
                    {
                        Room roomForSearchindId = new Room();
                        roomForSearchindId.id = context.Rooms.Max(item => item.id);
                        context.Rooms.Add(new Room() { id = roomForSearchindId.id, available = (obj as RoomCustom).available, fk_type = fkForRoomType });
                        context.SaveChanges();
                        RoomsCustom.Add(GetSpecifiedIdUserCustom(roomForSearchindId.id));
                    }
                }
            }
        }
        public void ModifyAvailabilityRoom(object obj)
        {

            if (obj == null)
            {
                MessageBox.Show("Selecteaza o camera");
            }
            else
            {
                Room room = new Room()
                {
                    id = (obj as RoomCustom).id,
                    available = (obj as RoomCustom).available
                };
                context.UpdateAvailabilityRoom(room.id, room.available);
                context.SaveChanges();
            }
        }

        public double GetDiscountedPriceWParameters(double oldPrice, int discount)
        {
            return oldPrice - ((double)((double)discount / 100) * oldPrice);
        }

        public double GetPriceForSpecifiecIdRoom(int roomId)
        {
            List<Room_type> room_Types = context.Room_type.ToList();
            List<Room> rooms = context.Rooms.ToList();

            foreach (Room room in rooms)
            {
                if (room.id == roomId)
                {
                    foreach (Room_type room_type in room_Types)
                    {
                        if (room_type.id == room.fk_type)
                        {
                            return room_type.price;
                        }
                    }
                }
            }
            return -1;

        }
    }
}

