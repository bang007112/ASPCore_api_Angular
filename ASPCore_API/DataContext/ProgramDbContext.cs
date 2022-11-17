using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASPCore_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ASPCore_API.DataContext
{
    public class ProgramDbContext:IdentityDbContext<Microsoft.AspNetCore.Identity.IdentityUser>
    {
        public ProgramDbContext() { }
        public ProgramDbContext(DbContextOptions<ProgramDbContext> options) : base(options) { }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
