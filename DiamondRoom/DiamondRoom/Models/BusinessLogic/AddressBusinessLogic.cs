using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class AddressBusinessLogic
    {
        private DiamondRoomEntities7 context = new DiamondRoomEntities7();
        public ObservableCollection<Models.Address> Addresses { get; set; }

        public string ErrorMessage { get; set; }

        public AddressBusinessLogic()
        {
            Addresses = new ObservableCollection<Address>();
        }
        public int AddAddress(string country, string city, string street)
        {
            Address newAddress = new Address();
            newAddress.country = country;
            newAddress.city = city;
            newAddress.street = street;

            if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street))
            {
                MessageBox.Show("Toate campurile sunt obligatorii!");
                return -1;
            }
            else
            {
                //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                //fara a utiliza procedura stocata AddPerson
                context.Addresses.Add(new Address() { country = country, city = city, street = street });
                context.SaveChanges();
                newAddress.id = context.Addresses.Max(item => item.id);
                Addresses.Add(newAddress);
                ErrorMessage = "";
                Console.WriteLine("New address successfully added to the database");
                return newAddress.id;
            }
        }

        public ObservableCollection<Address> GetAllAddresses()
        {
            List<Address> addresses = context.Addresses.ToList();
            ObservableCollection<Address> result = new ObservableCollection<Address>();
            foreach (Address con in addresses)
            {
                result.Add(con);
            }
            return result;
        }
        public int GetAddressWithSpecifiedInfo(string country, string city, string street)
        {
            List<Address> addressesList = context.Addresses.ToList();
            foreach (Address adr in addressesList)
            {
                if (adr.country.Equals(country) && adr.city.Equals(city) && adr.street.Equals(street))
                {
                    return adr.id;
                }
            }
            return -1;
        }
        public void AddAddress(object obj)
        {
            Address adr = obj as Address;
            if (adr != null)
            {
                if (string.IsNullOrEmpty(adr.country) || string.IsNullOrEmpty(adr.city) || string.IsNullOrEmpty(adr.street))
                {
                    ErrorMessage = "Completeaza toate campurile!";
                    Console.WriteLine("Completeaza toate campurile!");
                }
                else
                {
                    //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                    //fara a utiliza procedura stocata AddPerson
                    context.Addresses.Add(new Address() { country = adr.country, city = adr.city, street = adr.street});
                    context.SaveChanges();
                    adr.id = context.Addresses.Max(item => item.id);
                    Addresses.Add(adr);
                    ErrorMessage = "";
                }
            }
        }
    }
}
