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
    
    public partial class Restaurant
    {
        public Restaurant()
        {
            this.Dishes = new HashSet<Dish>();
        }
    
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public Nullable<int> LocId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual Geolocation Geolocation { get; set; }
    }
}