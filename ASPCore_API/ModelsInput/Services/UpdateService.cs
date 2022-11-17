using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPCore_API.ModelsInput.Services
{
    public class UpdateService
    {
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
    }
}
