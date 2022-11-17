using AutoMapper;
using ASPCore_API.DTOs;
using ASPCore_API.Models;
using ASPCore_API.ModelsInput.Rooms;
using System.Linq;

namespace Dental_Clinic_NET.API.AutoMapperProfiles
{
    public class RoomAutoMapperProfile : Profile
    {
        public RoomAutoMapperProfile()
        {
            CreateMap<CreateRoom, Room>();
            CreateMap<Room, RoomDTO>()
                .ForMember(des => des.DeviceNames, act => act.MapFrom(src => src.Devices.Select(d => d.DeviceName).ToList()))
                .ForMember(des => des.RoomType, act => act.MapFrom(src => src.RoomType.ToString()));
        }
    }
}
