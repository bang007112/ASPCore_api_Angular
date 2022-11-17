using System.Collections.Generic;

namespace ASPCore_API.ModelsInput.Services
{
    public class AddDevice
    {
        public int Id { get; set; }
        public IList<int> ListDeviceId { get; set; }
    }
}
