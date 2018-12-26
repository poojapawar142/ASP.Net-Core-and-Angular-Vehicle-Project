using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using WebApplicationWithCore.Models;
namespace WebApplicationWithCore.Controllers.Resources
{
    
    public class SaveVehicleResources
    {
        public int Id {get;set;}
        public int ModelId {get;set;}
        public bool IsRegistered {get;set;}
        [Required]
        public ContactResource Contact{get;set;}
       
        public ICollection<int> Features{get;set;}
        public SaveVehicleResources()
        {
            Features = new Collection<int>();
            
        }
    }
}