﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPCore_API.Models
{
    public class Room
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RoomCode { get; set; }
        public string Description { get; set; }
        public ICollection<Device> Devices { get; set; }

        public RoomTypes RoomType { get; set; } = 0;
        
        public enum RoomTypes
        {
            GeneralRoom,
            SpecificationRoom,
        }
    }
}
