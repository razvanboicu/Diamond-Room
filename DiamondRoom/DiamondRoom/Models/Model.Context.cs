﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiamondRoom.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DiamondRoomEntities1 : DbContext
    {
        public DiamondRoomEntities1()
            : base("name=DiamondRoomEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Extra_features> Extra_features { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Room_images> Room_images { get; set; }
        public virtual DbSet<Room_reservation_extra_features> Room_reservation_extra_features { get; set; }
        public virtual DbSet<Room_reservations> Room_reservations { get; set; }
        public virtual DbSet<Room_type> Room_type { get; set; }
        public virtual DbSet<Rooms_features> Rooms_features { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual int AddAdress(ObjectParameter id, string country, string city, string street)
        {
            var countryParameter = country != null ?
                new ObjectParameter("country", country) :
                new ObjectParameter("country", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("city", city) :
                new ObjectParameter("city", typeof(string));
    
            var streetParameter = street != null ?
                new ObjectParameter("street", street) :
                new ObjectParameter("street", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddAdress", id, countryParameter, cityParameter, streetParameter);
        }
    
        public virtual int AddContact(ObjectParameter id, string email, string phoneNr1, string phoneNr2)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var phoneNr1Parameter = phoneNr1 != null ?
                new ObjectParameter("phoneNr1", phoneNr1) :
                new ObjectParameter("phoneNr1", typeof(string));
    
            var phoneNr2Parameter = phoneNr2 != null ?
                new ObjectParameter("phoneNr2", phoneNr2) :
                new ObjectParameter("phoneNr2", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddContact", id, emailParameter, phoneNr1Parameter, phoneNr2Parameter);
        }
    
        public virtual int AddFeature(ObjectParameter id, string description)
        {
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddFeature", id, descriptionParameter);
        }
    
        public virtual int AddUser(ObjectParameter id, string username, string password, string firstname, string lastname, Nullable<int> acceslevel, Nullable<int> fkcontact, Nullable<int> fkaddress)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            var firstnameParameter = firstname != null ?
                new ObjectParameter("firstname", firstname) :
                new ObjectParameter("firstname", typeof(string));
    
            var lastnameParameter = lastname != null ?
                new ObjectParameter("lastname", lastname) :
                new ObjectParameter("lastname", typeof(string));
    
            var acceslevelParameter = acceslevel.HasValue ?
                new ObjectParameter("acceslevel", acceslevel) :
                new ObjectParameter("acceslevel", typeof(int));
    
            var fkcontactParameter = fkcontact.HasValue ?
                new ObjectParameter("fkcontact", fkcontact) :
                new ObjectParameter("fkcontact", typeof(int));
    
            var fkaddressParameter = fkaddress.HasValue ?
                new ObjectParameter("fkaddress", fkaddress) :
                new ObjectParameter("fkaddress", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddUser", id, usernameParameter, passwordParameter, firstnameParameter, lastnameParameter, acceslevelParameter, fkcontactParameter, fkaddressParameter);
        }
    
        public virtual int DeleteContact(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteContact", idParameter);
        }
    }
}
