using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using WebApplicationWithCore.Models;
namespace WebApplicationWithCore.Controllers.Resources
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource>Models {get;set;}
        public MakeResource ()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}