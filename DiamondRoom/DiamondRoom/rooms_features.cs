//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiamondRoom
{
    using System;
    using System.Collections.Generic;
    
    public partial class rooms_features
    {
        public int id { get; set; }
        public int fk_room { get; set; }
        public int fk_feature { get; set; }
    
        public virtual feature feature { get; set; }
        public virtual room room { get; set; }
    }
}
