using System.Collections.Generic;

namespace ASPCore_API.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string RoomCode { get; set; }
        public string Description { get; set; }
        public List<string> DeviceNames { get; set; }
        public string RoomType { get; set; }
    }
}
