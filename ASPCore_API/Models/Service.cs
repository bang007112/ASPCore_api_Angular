using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ASPCore_API.Models
{
    public class Service
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ServiceCode { get; set; }
        public string Description { get; set; }
        public int price { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
