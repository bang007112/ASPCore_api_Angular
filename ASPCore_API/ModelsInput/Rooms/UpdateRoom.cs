using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCore_API.ModelsInput.Rooms
{
    public class UpdateRoom
    {
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string RoomCode { get; set; }
        public string Description { get; set; }
    }
}
