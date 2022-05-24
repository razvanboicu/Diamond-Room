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

        private RoomTypeBusinessLogic roomTypeBusinessLogic = new RoomTypeBusinessLogic();
        public RoomBusinessLogic()
        {
            Rooms = new ObservableCollection<Room>();
            RoomsCustom = new ObservableCollection<RoomCustom>();
        }
        
        public bool CheckIfAnIdExists(int idSearched)
        {
            List<Room> rooms = context.Rooms.ToList();
            foreach(Room room in rooms)
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
            foreach(Room room in rooms)
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
                            price = (int) roomType.price
                        }) ;
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
                if(room.id == idSearched)
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
    }
}
