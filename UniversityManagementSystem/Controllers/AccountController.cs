using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Dto.AccountDto;
using UniversityManagementSystem.Interfaces;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }


        [HttpPost("login")]
        [Authorize]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUser != null)
                    return BadRequest(new { message = "Username already exists. Please choose a different one." });

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                            );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("delete_account")]
        [Authorize]
        public async Task<IActionResult> Delete([FromBody] DeleteUserDto deleteUserDto)
        {
            try
            {//validate input
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                //find user
                var user = await _userManager.FindByEmailAsync(deleteUserDto.Email);
                if (user == null) return NotFound("User not found.");
                //check password
                var passwordCheck = await _signInManager.CheckPasswordSignInAsync(user, deleteUserDto.Password, false);
                if (!passwordCheck.Succeeded) 
                    return Unauthorized("Invalid password.");
                //delete user
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return StatusCode(500, result.Errors);

                return Ok("User deleted successfully.");
            }
            catch (Exception e) 
            { 
                return StatusCode(500, e);
            }
        }

    }
}
