using ASPCore_API.DTOs;
using ASPCore_API.ModelsInput.Devices;
using ASPCore_API.ModelsInput.Rooms;
using ASPCore_API.ModelsInput.Services;
using ASPCore_API.Models;
using ASPCore_API.DataContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dental_Clinic_NET.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private ProgramDbContext _context;
        public IMapper AutoMapper { get; set; }
        public ServiceController(ProgramDbContext context, IMapper autoMapper)
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
                var services = _context.Services.Include(d => d.Devices).ToList();

                var serviceDTOs = services.Select(services => AutoMapper.Map<ServiceDTO>(services));

                return Ok(serviceDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        ///     Create new services from any actor
        /// </summary>
        /// <param name="request">Services Info</param>
        /// <returns>
        ///     200: Create success
        ///     500: Server handle error
        /// </returns>
        [HttpPost]
        public IActionResult Create(CreateService request)
        {
            try
            {
                Service service = AutoMapper.Map<Service>(request);
                _context.Services.Add(service);
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);

                return Ok(service);

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
                Service service = _context.Services.Find(id);
                if (service == null) return NotFound("Service not found.");

                ServiceDTO serviceDTO = AutoMapper.Map<ServiceDTO>(service);

                return Ok(serviceDTO);
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
                Service service = _context.Services.Find(id);
                if (service == null)
                {
                    return NotFound("service not found");
                }

                _context.Entry(service).State = EntityState.Deleted;
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
        public IActionResult Update(UpdateService request)
        {
            try
            {
                Service service = _context.Services.Find(request.Id);
                if (service == null)
                {
                    return NotFound("Service not found");
                }

                AutoMapper.Map<UpdateService, Service>(request, service);
                
                _context.Entry(service).State = EntityState.Modified;
                _context.SaveChanges();

                Console.WriteLine("Response done at: " + DateTime.Now);
                return Ok($"Update service success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
