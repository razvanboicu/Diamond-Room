//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Rooms_features
    {
        public int id { get; set; }
        public int fk_room { get; set; }
        public int fk_feature { get; set; }
        public virtual Feature Feature { get; set; }
        public virtual Room Room { get; set; }
    }
}
