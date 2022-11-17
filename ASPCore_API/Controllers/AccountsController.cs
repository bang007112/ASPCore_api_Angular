using ASPCore_API.DataContext;
using ASPCore_API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ASPCore_API.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using ASPCore_API.ModelsInput.Account;

namespace ASPCore_API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IConfiguration _configuration;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public AccountsController(IConfiguration configuration,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InputUsers user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new
                    {
                        code = HttpStatusCode.BadRequest,
                        errors = new string[] { "Invalid Token" }
                    });
                }
                else{
                    IdentityUser userIdentity = new IdentityUser() { UserName = user.Username, Email = user.Email, PhoneNumber = user.PhoneNumber };
                    var createResult = await _userManager.CreateAsync(userIdentity, user.Password);
                    if(createResult.Succeeded)
                    {
                        return Ok("Sign Up Succeeded");
                    }
                    else
                    {
                        return BadRequest(createResult);
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Get(){
            try{ 
                return Ok(_userManager.Users);
            }catch(Exception ex){
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id){
            try{ 
                IdentityUser user  = await _userManager.FindByIdAsync(id); 
                return Ok(new UserDTO(){
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber
                });
            }catch(Exception ex){
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id, InputUsers inputUsers){
            try{ 
                if( await _userManager.FindByIdAsync(id) != null){
                   IdentityUser user = await _userManager.FindByIdAsync(id);
                   if(inputUsers.Username!=null && inputUsers.Username != "") user.UserName = inputUsers.Username;
                   
                   if(inputUsers.Email!=null && inputUsers.Email != "") user.Email = inputUsers.Email;
                   if(inputUsers.PhoneNumber!=null && inputUsers.PhoneNumber != "") user.PhoneNumber = inputUsers.PhoneNumber;
                    await _userManager.UpdateAsync(user);
                    if(inputUsers.Password!=null && inputUsers.Password != "") await _userManager.ChangePasswordAsync(user,user.PasswordHash,inputUsers.Password);
                   return Ok();
                }
                return BadRequest();
            }catch(Exception ex){
                return BadRequest();
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id){
            try{ 
                IdentityUser user = await _userManager.FindByIdAsync(id);
                if(user!=null){
                    await _userManager.DeleteAsync(user);
                    return Ok();
                }
                return BadRequest();
            }catch(Exception ex){
                return BadRequest();
            }
        }
        [HttpPost("/api/Accounts/Login")]
        public async Task<IActionResult> Login(Login account){
            try
            {
                IdentityUser user = await _userManager.FindByNameAsync(account.Username);
                if (user != null)
                {
                    bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, account.Password);
                    if (isPasswordCorrect)
                    {
                        var authClaims = new List<Claim>
                        {
                            new Claim("Id", user.Id),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                        var token = new JwtSecurityToken(issuer: _configuration["Jwt:ValidIssuer"],
                                audience: _configuration["Jwt:ValidAudience"],
                                expires: DateTime.Now.AddHours(3),
                                claims: authClaims,
                                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                        return Ok(new
                        {
                            token = token.EncodedPayload
                        });
                    }
                }
                return Unauthorized("UserName or Password incorrect...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
