using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplicationWithCore.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        public int Id {get;set;}
        public int ModelId {get;set;}
        public Model model{get;set;}
        public bool IsRegistered {get;set;}
        [Required]
        [StringLength(255)]
        public string ContactName {get;set;}
        [StringLength(255)]
        public string ContactEmail {get;set;}
        [Required]
        [StringLength(255)]
        public string ContactPhone {get;set;}
        public DateTime LastUpdate{get;set;} 
        public ICollection<VehicleFeature> Features{get;set;}
        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            
        }


    }
}