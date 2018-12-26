using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace WebApplicationWithCore.Controllers.Resources
{
    public class VehicleResource
    {
        
        public int Id {get;set;}
        
        public KeyValuePairResource model{get;set;}
        public bool IsRegistered {get;set;}
        public KeyValuePairResource make{get;set;}
        public ContactResource Contact {get;set;}
         public DateTime LastUpdate{get;set;} 
        public ICollection<KeyValuePairResource> Features{get;set;}
        public VehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
            
        }

    }
}