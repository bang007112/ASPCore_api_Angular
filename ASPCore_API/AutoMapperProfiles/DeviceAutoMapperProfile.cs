using AutoMapper;
using ASPCore_API.DTOs;
using ASPCore_API.ModelsInput.Devices;
using ASPCore_API.Models;
using System;
using System.Linq;

namespace Dental_Clinic_NET.API.AutoMapperProfiles
{
    public class DeviceAutoMapperProfile : Profile
    {
        public DeviceAutoMapperProfile()
        {
            CreateMap<CreateDevice, Device>();
            CreateMap<Device, DeviceDTO>()
                .ForMember(des => des.ServiceNames, act => act.MapFrom(src => src.Services.Select(d => d.ServiceCode).ToList()));
            CreateMap<UpdateDevice, Device>()
                .ForAllMembers(opt => opt.Condition((src, des, field) =>
                {
                    bool condition_01 = field is string && !string.IsNullOrWhiteSpace(field.ToString());
                    bool condition_02 = field is int && int.Parse(field.ToString()) > 0;
                    return condition_01 || condition_02;
                }));
        }
        
    }
}
