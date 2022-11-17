using ASPCore_API.DTOs;
using ASPCore_API.ModelsInput.Devices;
using ASPCore_API.ModelsInput.Rooms;
using ASPCore_API.ModelsInput.Services;
using ASPCore_API.Models;
using ASPCore_API.DataContext;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic_NET.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private ProgramDbContext _context;
        public IMapper AutoMapper { get; set; }
        public RoomController(ProgramDbContext context, IMapper autoMapper)
        {
            _context = context;
            AutoMapper = autoMapper;
        }
        /// <summary>
        ///     List all rooms by admin
        /// </summary>
        /// <returns>
        ///     200: Request success
        ///     500: Server Handle Error
        ///     
        /// </returns>
        [HttpGet]
        public IActionResult GetAll(int page = 1)
        {
            try
            {
                var rooms = _context.Rooms.Include(r => r.Devices).ToList();
                var roomDTOs = rooms.Select(room => AutoMapper.Map<RoomDTO>(room));

                return Ok(roomDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Create new room from any actor
        /// </summary>
        /// <param name="request">Room Info</param>
        /// <returns>
        ///     200: Create success
        ///     500: Server handle error
        /// </returns>
        [HttpPost]
        public IActionResult Create(CreateRoom request)
        {
            try
            {
                Room room = AutoMapper.Map<Room>(request);
                if(_context.Rooms.FirstOrDefault(r => r.RoomCode == room.RoomCode) != null) return BadRequest("Duplicate room code");
                _context.Rooms.Add(room);
                _context.SaveChanges();
                RoomDTO roomDTO = AutoMapper.Map<RoomDTO>(room);

                Console.WriteLine("Response done at: " + DateTime.Now);

                return Ok(roomDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Get a Room details
        /// </summary>
        /// <param name="id">room id</param>
        /// <returns>
        ///     200: Request success
        ///     500: Server handle error
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Room room = _context.Rooms.Find(id);
                if (room == null) return NotFound("Room not found.");

                RoomDTO roomDTO = AutoMapper.Map<RoomDTO>(room);

                return Ok(roomDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Remove room out of database
        /// </summary>
        /// <param name="id">room id</param>
        /// <returns>
        ///     200: Request success
        ///     500: Server handle error
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Room room = _context.Rooms.Find(id);
                if (room == null)
                {
                    return NotFound("Room not found");
                }

                _context.Entry(room).State = EntityState.Deleted;
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);

                return Ok($"You just have completely delete room with id='{id}' success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(UpdateRoom request)
        {
            try
            {
                Room room = _context.Rooms.Find(request.Id);
                if (room == null)
                {
                    return NotFound("Room not found");
                }
                if(request.RoomCode != null && request.RoomCode != "") room.RoomCode = request.RoomCode;
                if(request.Description != null && request.Description != "") room.Description = request.Description;
                _context.Entry(room).State = EntityState.Modified;
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);
                return Ok($"Update room success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
