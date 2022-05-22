﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiamondRoom.Models.BusinessLogic
{
    public class UserBusinessLogic
    {
        //private ContactBusinessLogic contactBusinessLogic;
        //private AddressBusinessLogic addressBusinessLogic;
        private DiamondRoomEntities1 context = new DiamondRoomEntities1();
        public ObservableCollection<User> Users { get; set; }

        public string ErrorMessage { get; set; }

        public UserBusinessLogic()
        {
            Users = new ObservableCollection<User>();
        }
        public User CheckIfUserExists(string usernameTyped, string passwordTyped)
        {
            List<User> users = context.Users.ToList();
            foreach (User user in users)
            {
                if(user.username.Equals(usernameTyped) && user.password.Equals(passwordTyped))
                {
                    Console.WriteLine("User found");
                    return user;
                }
            }
            Console.WriteLine("nu a gasit pt"+usernameTyped+"\n"+passwordTyped);
            return null;
        
        }
        public void AddUser(string userr, string pass, string firstname, string lastname, int fkAdr, int fkCont)
        {
            User newUser = new User();
            newUser.username = userr;
            newUser.password = pass;
            newUser.firstName = firstname;
            newUser.lastName = lastname;
            newUser.fk_address = fkAdr;
            newUser.fk_contact = fkCont;
            newUser.accessLevel = 2;

            if (string.IsNullOrEmpty(newUser.username) || string.IsNullOrEmpty(newUser.password) || string.IsNullOrEmpty(newUser.firstName) || string.IsNullOrEmpty(newUser.lastName))
            {
                MessageBox.Show("Toate campurile sunt obligatorii!");
                //Console.WriteLine("Completeaza toate campurile!");
            }
            else
            {
                context.Users.Add(new User() { username = newUser.username, password = newUser.password, firstName = newUser.firstName, lastName = newUser.lastName, accessLevel = 2, fk_address = fkAdr, fk_contact = fkCont });
                context.SaveChanges();
                newUser.id = context.Users.Max(item => item.id);
                Users.Add(newUser);
                ErrorMessage = "";
                Console.WriteLine("New USER(client) successfully added to the database");
            }
        }
       
    }
}
