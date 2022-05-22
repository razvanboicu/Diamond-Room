using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class ContactBusinessLogic
    {
        private DiamondRoomEntities1 context = new DiamondRoomEntities1();
        public ObservableCollection<Contact> Contacts { get; set; }

        public string ErrorMessage { get; set; }

        public ContactBusinessLogic()
        {
            Contacts = new ObservableCollection<Contact>();
        }

        public int AddContact(string ema, string phone1, string phone2)
        {
            Contact newContact = new Contact();
            newContact.email = ema;
            newContact.phoneNr1 = phone1;
            newContact.phoneNr2 = phone2;
            if (string.IsNullOrEmpty(newContact.email) || string.IsNullOrEmpty(newContact.phoneNr1) || string.IsNullOrEmpty(newContact.phoneNr2))
            {
                MessageBox.Show("Toate campurile sunt obligatorii!");
                return -1;
            }
            else
            {
                //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                //fara a utiliza procedura stocata AddPerson
                context.Contacts.Add(new Contact() { email = newContact.email, phoneNr1 = newContact.phoneNr1, phoneNr2 = newContact.phoneNr2 });
                context.SaveChanges();
                newContact.id = context.Contacts.Max(item => item.id);
                Contacts.Add(newContact);
                ErrorMessage = "";
                Console.WriteLine("New contact successfully added to the database");
                return newContact.id;
            }

        }
        public void AddMethod(object obj)
        {
            Contact contact = obj as Contact;
            if (contact != null)
            {
                if (string.IsNullOrEmpty(contact.email) || string.IsNullOrEmpty(contact.phoneNr1) || string.IsNullOrEmpty(contact.phoneNr2))
                {
                    ErrorMessage = "Trebuiesc completate toate campurile";
                    Console.WriteLine("Trebuiesc completate toate campurile");
                }
                else
                {
                    //context.AddPerson(pers.Name, pers.Address, new ObjectParameter("persId", pers.PersonID));
                    //fara a utiliza procedura stocata AddPerson
                    context.Contacts.Add(new Contact() { email = contact.email, phoneNr1 = contact.phoneNr1, phoneNr2 = contact.phoneNr2 });
                    context.SaveChanges();
                    contact.id = context.Contacts.Max(item => item.id);
                    Contacts.Add(contact);
                    ErrorMessage = "";
                }
            }

        }
        public void DeleteMethod(object obj)
        {
            Contact contact = obj as Contact;
            if (contact == null)
            {
                ErrorMessage = "Selecteaza un contact!";
            }
            else
            {
                    context.Contacts.Remove(contact);
                    context.SaveChanges();
                    Contacts.Remove(contact);
                    ErrorMessage = "";
            }
        }

        public void UpdateMethod(object obj)
        {
            Contact con = obj as Contact;
            if (con == null)
            {
                ErrorMessage = "Selecteaza un contact!";
            }
            else if (string.IsNullOrEmpty(con.email) && string.IsNullOrEmpty(con.phoneNr1) && string.IsNullOrEmpty(con.phoneNr2))
            {
                ErrorMessage = "Precizeaza noile campuri!";
            }
            else
            {
                context.ModifyContact(con.id, con.email, con.phoneNr1, con.phoneNr2);
                context.SaveChanges();
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

        public int GetContactWithSpecifiedInfo(string email, string phone1, string phone2)
        {
            List<Contact> contacts = context.Contacts.ToList();
            foreach (Contact con in contacts)
            {
                if(con.email.Equals(email) && con.phoneNr1.Equals(phone1) && con.phoneNr2.Equals(phone2))
                {
                    return con.id;
                }
            }
            return -1;
        }
    }
}
