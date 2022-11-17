using AutoMapper;
using ASPCore_API.DTOs;
using ASPCore_API.ModelsInput.Devices;
using ASPCore_API.ModelsInput.Rooms;
using ASPCore_API.ModelsInput.Services;
using ASPCore_API.Models;
using System.Linq;

namespace Dental_Clinic_NET.API.AutoMapperProfiles
{
    public class ServiceAutoMapperProfile : Profile
    {
        public ServiceAutoMapperProfile()
        {
            CreateMap<CreateService, Service>();
            CreateMap<Service, ServiceDTO>()
                .ForMember(des => des.DeviceNames, act => act.MapFrom(src => src.Devices.Select(d => d.DeviceName).ToList()))
                .ForMember(des => des.DeviceIdList, act => act.MapFrom(src => src.Devices.Select(d => d.Id).ToList()));
            CreateMap<UpdateService, Service>()
                    .ForAllMembers(opt => opt.Condition((src, des, field) =>
                    {
                        bool condition_01 = field is string && !string.IsNullOrWhiteSpace(field.ToString());
                        bool condition_02 = field is int && int.Parse(field.ToString()) > 0;
                        return condition_01 || condition_02;
                    }));
        }
    }
}
