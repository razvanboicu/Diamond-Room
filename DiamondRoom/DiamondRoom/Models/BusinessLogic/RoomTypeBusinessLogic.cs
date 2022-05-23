using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class RoomTypeBusinessLogic
    {
        private DiamondRoomEntities6 context = new DiamondRoomEntities6();
        public ObservableCollection<Room_type> RoomTypes { get; set; }
        public RoomTypeBusinessLogic()
        {
            RoomTypes = new ObservableCollection<Room_type>();
        }

        public int GetIdForSearchedType(string typeSearched)
        {
            List<Room_type> roomTypes = context.Room_type.ToList();
            foreach(Room_type roomType in roomTypes)
            {
                if (roomType.type.Equals(typeSearched))
                {
                    return roomType.id;
                }
            }
            return -1;
        }
        public ObservableCollection<Room_type> GetAllRoomTypes()
        {
            List<Room_type> roomTypes = context.Room_type.ToList();
            ObservableCollection<Room_type> result = new ObservableCollection<Room_type>();
            foreach(Room_type roomType in roomTypes)
            {
                result.Add(roomType);
            }
            return result;
        }
        public int GetIdForSpecifiedType(string typeSearched)
        {
            List<Room_type> roomTypes = context.Room_type.ToList();
            foreach (Room_type roomType in roomTypes)
            {
                if (roomType.type.Equals(typeSearched))
                    return roomType.id;
            }
            return -69;
        }
        public void AddRoomType(object obj)
        {
            Room_type newRoomType = obj as Room_type;
            if (newRoomType != null)
            {
                if (string.IsNullOrEmpty(newRoomType.type) && newRoomType.price != 0.0f)
                {
                    MessageBox.Show("Completeaza corespunzator campurile!");
                }
                else
                {
                    //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                    //fara a utiliza procedura stocata AddPerson
                    context.Room_type.Add(new Room_type() { price = newRoomType.price, type = newRoomType.type});
                    context.SaveChanges();
                    newRoomType.id = context.Room_type.Max(item => item.id);
                    RoomTypes.Add(newRoomType);
                }
            }
        }

        public void ModifyRoomType(object obj)
        {
            Room_type roomFeature = obj as Room_type;
            if (roomFeature == null)
            {
                MessageBox.Show("Selecteaza o optiune!");
            }
            else if (string.IsNullOrEmpty(roomFeature.type) && roomFeature.price != 0.0f)
            {
                MessageBox.Show("Precizeaza corespunzator noile campuri!");
            }
            else
            {
                context.ModifyRoomType(roomFeature.id, roomFeature.type, roomFeature.price);
                context.SaveChanges();
            }
        }

        public void DeleteMethod(object obj)
        {
            Room_type feature = obj as Room_type;
            if (feature == null)
            {
                MessageBox.Show("Selecteaza un tip de camera pentru stergere!");
            }
            else
            {
                context.Room_type.Remove(feature);
                context.SaveChanges();
                RoomTypes.Remove(feature);
            }
        }

    }
}
