using AutoMapper;
using System.Linq;
using WebApplicationWithCore.Models;
using System.Collections.Generic;
using WebApplicationWithCore.Controllers.Resources;

namespace WebApplicationWithCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to API Resource
            CreateMap<Make , MakeResource>();
            CreateMap<Make , KeyValuePairResource>();
            CreateMap<Model , KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResources>()
            .ForMember(vr=>vr.Contact,opt=>opt.MapFrom(v=>new ContactResource{Name = v.ContactName , Email = v.ContactEmail , Phone = v.ContactPhone}))
            .ForMember(vr=>vr.Features,opt=>opt.MapFrom(v=>v.Features.Select(vf=>vf.FeatureId)));
            CreateMap<Vehicle,VehicleResource>()
            .ForMember(vr=>vr.make , opt=>opt.MapFrom(v=>v.model.Make))
            .ForMember(vr=>vr.Contact,opt=>opt.MapFrom(v=>new ContactResource{Name = v.ContactName , Email = v.ContactEmail , Phone = v.ContactPhone}))
            .ForMember(vr=>vr.Features,opt=>opt.MapFrom(v=>v.Features.Select(vf=> new KeyValuePairResource {Id = vf.Feature.Id , Name = vf.Feature.Name})));
            //API Resource to Domain
            CreateMap<SaveVehicleResources, Vehicle>()
            .ForMember(v=>v.Id,opt=>opt.Ignore()) // To ignore mapping of this property
            .ForMember(v=>v.ContactName,opt=>opt.MapFrom(vr=>vr.Contact.Name))
            .ForMember(v=>v.ContactEmail,opt=>opt.MapFrom(vr=>vr.Contact.Email))
            .ForMember(v=>v.ContactPhone,opt=>opt.MapFrom(vr=>vr.Contact.Phone))
            .ForMember(v=>v.Features,opt=>opt.Ignore())
            .AfterMap((vr,v)=>{
                //Remove unselected feature
             
                var removeFeatures = v.Features.Where(f=>!vr.Features.Contains(f.FeatureId));
                foreach(var f in removeFeatures )
                v.Features.Remove(f);
              
                //Add new features
              
                var addedFeature = vr.Features.Where(id=>!v.Features.Any(f=>f.FeatureId ==id)).Select(id=>new VehicleFeature{FeatureId = id});
                foreach(var f in addedFeature)
                v.Features.Add(f);

            });
        }
    }
}