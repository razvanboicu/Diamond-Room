using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class ReservationBusinessLogic
    {
        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Reservation> Reservations { get; set; }
        public ObservableCollection<ReservationCustom> ReservationsCustom { get; set; }
        public ReservationBusinessLogic()
        {
            Reservations = new ObservableCollection<Reservation>();
            ReservationsCustom = new ObservableCollection<ReservationCustom>();
        }
        public void ModifyStatusReservation(object obj)
        {

            ReservationCustom con = obj as ReservationCustom;
            int idReservation = con.idReservation;
            string newStatus = con.status;
            if (con == null)
            {
                MessageBox.Show("Selecteaza o rezervare");
            }
            else if (string.IsNullOrEmpty(con.status))
            {
                MessageBox.Show("Precizeaza corespunzator campul STATUS");
            }
            else
            {
                context.ModifyStatusRoomReservation(idReservation, newStatus);
                context.SaveChanges();
            }
        }
        public ObservableCollection<ReservationCustom> GetAllCustomReservationsForSpecificUserId(int id)
        {
            List<Reservation> reservations = context.Reservations.ToList();
            List<Room_reservations> roomReservations = context.Room_reservations.ToList();
            List<Room> rooms = context.Rooms.ToList();
            List<User> users = context.Users.ToList();
            List<Offer> offers = context.Offers.ToList();
            List<Room_reservation_extra_features> roomReservationExtraFeatures = context.Room_reservation_extra_features.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();
            List<Extra_features> extraFeatures = context.Extra_features.ToList();

            ObservableCollection<ReservationCustom> result = new ObservableCollection<ReservationCustom>();

            foreach (Reservation reservation in reservations)
            {
                if(reservation.fk_user == id)
                {
                    ReservationCustom reservationCustom = new ReservationCustom();
                    int reservationId = reservation.id;
                    int userId = id;
                    int fkType = -1;
                    double pricee = 0;
                    string firstNamee = "", lastNamee = "", roomTypee = "";
                    foreach (Room_reservations roomReservation in roomReservations)
                    {
                        if (roomReservation.fk_reservation == reservationId)
                        {
                            if (roomReservation.deleted == false)
                            {
                                foreach (User user in users)
                                {
                                    if (user.id == userId)
                                    {
                                        firstNamee = user.firstName;
                                        lastNamee = user.lastName;
                                        break;
                                    }
                                }

                                bool hasReservation = false;
                                foreach (Room room in rooms)
                                {
                                    if (room.id == roomReservation.fk_room)
                                    {
                                        fkType = room.fk_type;

                                        break;
                                    }
                                }

                                foreach (Room_type room_Type in roomTypes)
                                {
                                    if (room_Type.id == fkType)
                                    {
                                        roomTypee = room_Type.type;
                                        pricee = room_Type.price;
                                        break;
                                    }
                                }

                                int extraPrice = 0;
                                foreach (Room_reservation_extra_features room_Reservation_Extra_Features in roomReservationExtraFeatures)
                                {
                                    if (room_Reservation_Extra_Features.fk_room_reservation == reservation.id)
                                    {
                                        foreach (Extra_features extraFeature in extraFeatures)
                                        {
                                            if (extraFeature.id == room_Reservation_Extra_Features.fk_extra_feature)
                                            {
                                                extraPrice += (int)extraFeature.price;
                                            }
                                        }
                                    }
                                }

                                reservationCustom.firstName = firstNamee;
                                reservationCustom.lastName = lastNamee;
                                reservationCustom.roomNumber = roomReservation.fk_room;
                                reservationCustom.roomType = roomTypee;
                                reservationCustom.dateFrom = reservation.dateFrom;
                                reservationCustom.dateTo = reservation.dateTo;
                                reservationCustom.status = roomReservation.status;
                                reservationCustom.observations = roomReservation.observations;
                                reservationCustom.total = extraPrice + (int)pricee;
                                reservationCustom.idReservation = reservation.id;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    double nrOfDays = (reservation.dateTo - reservation.dateFrom).TotalDays;
                    reservationCustom.total *= (int)nrOfDays;
                    result.Add(reservationCustom);
                }
               
            }

            return result;
        }
        public ObservableCollection<ReservationCustom> GetAllCustomReservations()
        {
            List<Reservation> reservations = context.Reservations.ToList();
            List<Room_reservations> roomReservations = context.Room_reservations.ToList();
            List<Room> rooms = context.Rooms.ToList();
            List<User> users = context.Users.ToList();
            List<Room_reservation_extra_features> roomReservationExtraFeatures = context.Room_reservation_extra_features.ToList();
            List<Room_type> roomTypes = context.Room_type.ToList();
            List<Extra_features> extraFeatures = context.Extra_features.ToList();
            List<Offer> offers = context.Offers.ToList();

            ObservableCollection<ReservationCustom> result = new ObservableCollection<ReservationCustom>();

            foreach (Reservation reservation in reservations)
            {
                ReservationCustom reservationCustom = new ReservationCustom();
                int disc = -1;
                int reservationId = reservation.id;
                int userId = reservation.fk_user;
                int fkType = -1;
                double pricee = 0;
                string firstNamee = "", lastNamee = "", roomTypee = "";
                foreach (Room_reservations roomReservation in roomReservations)
                {
                    if (roomReservation.fk_reservation == reservationId)
                    {
                        if (roomReservation.deleted == false)
                        {
                            foreach (User user in users)
                            {
                                if (user.id == userId)
                                {
                                    firstNamee = user.firstName;
                                    lastNamee = user.lastName;
                                    break;
                                }
                            }

                            foreach (Room room in rooms)
                            {
                                if (room.id == roomReservation.fk_room)
                                {
                                    fkType = room.fk_type;
                                    foreach (Offer offer in offers)
                                    {
                                        if (room.id == offer.fk_room &&
                                            offer.deleted == false)
                                        {
                                            disc = offer.discount;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }

                            foreach (Room_type room_Type in roomTypes)
                            {
                                if (room_Type.id == fkType)
                                {
                                    roomTypee = room_Type.type;
                                    pricee = room_Type.price;
                                    break;
                                }
                            }

                            int extraPrice = 0;
                            foreach (Room_reservation_extra_features room_Reservation_Extra_Features in roomReservationExtraFeatures)
                            {
                                if (room_Reservation_Extra_Features.fk_room_reservation == reservation.id)
                                {
                                    foreach (Extra_features extraFeature in extraFeatures)
                                    {
                                        if (extraFeature.id == room_Reservation_Extra_Features.fk_extra_feature)
                                        {
                                            extraPrice += (int)extraFeature.price;
                                        }
                                    }

                                }

                            }

                            reservationCustom.firstName = firstNamee;
                            reservationCustom.lastName = lastNamee;
                            reservationCustom.roomNumber = roomReservation.fk_room;
                            reservationCustom.roomType = roomTypee;
                            reservationCustom.dateFrom = reservation.dateFrom;
                            reservationCustom.dateTo = reservation.dateTo;
                            reservationCustom.status = roomReservation.status;
                            reservationCustom.observations = roomReservation.observations;
                            reservationCustom.total = extraPrice + (int)pricee;
                            reservationCustom.idReservation = reservation.id;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                
                double nrOfDays = (reservation.dateTo - reservation.dateFrom).TotalDays;
                int totalPrice = reservationCustom.total *= (int)nrOfDays;
                if (disc != -1)
                {
                    int discountCalculated = totalPrice / disc;
                    totalPrice -= discountCalculated;
                }
                reservationCustom.total = (int)totalPrice;
                result.Add(reservationCustom);
            }

            return result;
        }
    }
}
