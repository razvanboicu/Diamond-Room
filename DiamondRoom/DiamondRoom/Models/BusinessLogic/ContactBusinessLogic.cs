using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondRoom.Models.BusinessLogic
{
    public class ContactBusinessLogic
    {
        private DiamondRoomEntities1 context = new DiamondRoomEntities1();
        public ObservableCollection<Contact> ContactList { get; set; }

        public string ErrorMessage { get; set; }

        public ContactBusinessLogic()
        {
            ContactList = new ObservableCollection<Contact>();
        }

        public void AddMethod(object obj)
        {
            //parametrul obj este cel dat prin CommandParameter cu MultipleBinding la Button in xaml
            Contact con = new Contact();
            //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
            //fara a utiliza procedura stocata AddPerson
            context.Contacts.Add(new Contact() { email = "test@gmail.com", phoneNr1 = "010101010", phoneNr2 = "010101010" });
            context.SaveChanges();
            con.id = context.Contacts.Max(item => item.id);
            ContactList.Add(con);


            int id = context.Contacts.Max(item => item.id);

        }
        public void DeleteMethod(object obj)
        {
           Contact contact = obj as Contact;
            if (contact != null)
                ErrorMessage = "Selecteaza un contact";
            else
            {
                context.DeleteContact(contact.id);
                context.SaveChanges();
                ContactList.Remove(contact);
                ErrorMessage = "";
            }
        }
        public ObservableCollection<Contact> GetAllContacts()
        {
            List<Contact> contacts = context.Contacts.ToList();
            ObservableCollection<Contact> result = new ObservableCollection<Contact>();
            foreach (Contact con in contacts)
            {
                result.Add(con);
            }
            return result;
        }
    }
}
