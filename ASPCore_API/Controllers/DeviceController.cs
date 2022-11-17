using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using ASPCore_API.DTOs;
using ASPCore_API.ModelsInput.Devices;
using ASPCore_API.ModelsInput.Rooms;
using ASPCore_API.ModelsInput.Services;
using ASPCore_API.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPCore_API.DataContext;
using AutoMapper;


namespace Dental_Clinic_NET.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private ProgramDbContext _context;
        public IMapper AutoMapper { get; set; }
        public DeviceController(ProgramDbContext context, IMapper autoMapper)
        {
            _context = context;
            AutoMapper = autoMapper;
        }
        /// <summary>
        ///     List all services by admin
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
                var devices = _context.Devices.Include(d => d.Services).ToList();
                var deviceDTOs = devices.Select(device => AutoMapper.Map<DeviceDTO>(device));
                return Ok(deviceDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Create new device from any actor
        /// </summary>
        /// <param name="request">Devices Info</param>
        /// <returns>
        ///     200: Create success
        ///     500: Server handle error
        /// </returns>
        [HttpPost]
        public IActionResult Create(CreateDevice request)
        {
            try
            {
                Device device = AutoMapper.Map<Device>(request);
                _context.Devices.Add(device);
                _context.SaveChanges();

                // Push event
                Console.WriteLine("Response done at: " + DateTime.Now);

                return Ok(device);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Get a Service details
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>
        ///     200: Request success
        ///     500: Server handle error
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Device device = _context.Devices.Find(id);
                if (device == null) return NotFound("Service not found.");

                DeviceDTO deviceDTO = AutoMapper.Map<DeviceDTO>(device);

                return Ok(deviceDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Remove service out of database
        /// </summary>
        /// <param name="id">service id</param>
        /// <returns>
        ///     200: Request success
        ///     500: Server handle error
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Device device = _context.Devices.Find(id);
                if (device == null)
                {
                    return NotFound("Device not found");
                }

                _context.Entry(device).State = EntityState.Deleted;
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);

                return Ok($"You just have completely delete service with id='{id}' success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Update(UpdateDevice request)
        {
            try
            {
                Device device = _context.Devices.Include(d => d.Services).FirstOrDefault(d => d.Id == request.Id);
                if (device == null)
                {
                    return NotFound("Service not found");
                }
                AutoMapper.Map<UpdateDevice, Device>(request, device);

                _context.Entry(device).State = EntityState.Modified;
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);
                return Ok($"Update device success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
