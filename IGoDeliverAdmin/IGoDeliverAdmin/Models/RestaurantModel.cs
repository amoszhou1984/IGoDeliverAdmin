using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IGoDeliverAdmin.Entity;

namespace IGoDeliverAdmin.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("餐馆名")]
        public string Name { get; set; }
        [DisplayName("联系人")]
        public string ContactName { get; set; }
        [DisplayName("联系电话")]
        public string ContactPhone { get; set; }
        [DisplayName("简介")]
        public string Descriptions { get; set; }
        public int LocationId { get; set; }
        [DisplayName("单元号")]
        public string UnitNumber { get; set; }
        [Required]
        [DisplayName("门牌号")]
        public int StreetNumber { get; set; }
        [Required]
        [DisplayName("街道")]
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string StateProvince { get; set; }
        public string Country { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}