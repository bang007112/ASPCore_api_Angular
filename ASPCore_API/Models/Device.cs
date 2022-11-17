﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPCore_API.Models
{
    public class Device
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DeviceValue { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; } = true;
        public ICollection<Service> Services { get; set; }

        [Column("room_id")]
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room {get;set;}
    }
}
