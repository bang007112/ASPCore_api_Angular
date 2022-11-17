﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ASPCore_API.DTOs
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public int DeviceValue { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; } = true;
        public List<string> ServiceNames { get; set; }
        public int RoomId { get; set; }
    }
}
