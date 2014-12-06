//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IGoDeliverAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Geolocation
    {
        public Geolocation()
        {
            this.Restaurants = new HashSet<Restaurant>();
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int LocId { get; set; }
        public int SuburbId { get; set; }
        public Nullable<double> Altitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string Street { get; set; }
        public int StreetNo { get; set; }
        public string UnitNo { get; set; }
    
        public virtual Suburb Suburb { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
