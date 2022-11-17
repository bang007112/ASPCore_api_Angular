using System.Collections.Generic;

namespace ASPCore_API.ModelsInput.Devices
{
    public class AddService
    {
        public int Id { get; set; }
        public IList<int> ListServiceId { get; set; }
    }
}
