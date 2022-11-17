using System.Collections.Generic;

namespace ASPCore_API.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<string> DeviceNames { get; set; }
        public List<int> DeviceIdList { get; set; }
    }
}
