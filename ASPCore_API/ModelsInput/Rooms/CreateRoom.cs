using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCore_API.ModelsInput.Rooms
{
    public class CreateRoom
    {
        [Required]
        [StringLength(50)]
        public string RoomCode { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
