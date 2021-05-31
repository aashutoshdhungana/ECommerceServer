using System;
using AutoMapper;
using System.Linq;
using ECommerceServer.Models;
using System.Threading.Tasks;
using ECommerceServer.Services;
using Microsoft.AspNetCore.Mvc;
using ECommerceServer.Models.DTO.User;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJWTAuthentication _auth;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IJWTAuthentication auth, IUserService userService, IMapper mapper)
        {
            _auth = auth;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthenticateRequest user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var validuser = await _userService.GetUserByEmailAsync(user.UserName);
                    if (!UserService.ValidateUser(validuser, user.PassWord))
                        return BadRequest("Incorrect Email or Password");

                    string token = _auth.Authenticate(validuser.Email);
                    var response = UserService.GetReponseUser(validuser, token);

                    return Ok(response);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return StatusCode(500);
                }
            }

            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Register")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserCreateDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userDTO.Password = AccountUtil.PasswordHasher(userDTO.Password);
                    var userModel = _mapper.Map<User>(userDTO);

                    await _userService.CreateUserAsync(userModel);
                    await _userService.SaveChangesAsync();

                    return Ok("You have been registered successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        [HttpPut]
        [Route("/Account/Update/{id:guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdateDTO user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var validUser = await _userService.GetUserByIdAsync(id);
                    if (validUser == null)
                        return NotFound();

                    var userModel = _mapper.Map(user, validUser);
                    _userService.UpdateUser(userModel);
                    await _userService.SaveChangesAsync();
                    return Ok("Your account has been updated successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        [HttpDelete]
        [Route("/Account/Delete/{id:guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userModel = await _userService.GetUserByIdAsync(id);
                    if (userModel == null)
                    {
                        return NotFound("User Not Found");
                    }
                    _userService.DeleteUser(userModel);
                    await _userService.SaveChangesAsync();

                    return Ok("Deleted Successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }

        [HttpPut]
        [Route("/Account/ChangePassword/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] UserPasswordChangeDOT userPasswordDOT)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User validUser = await _userService.GetUserByIdAsync(id);
                    if (!UserService.ValidateUser(validUser, userPasswordDOT.OldPassword))
                    {
                        return BadRequest("Incorret Old Password");
                    }

                    validUser.Password = AccountUtil.PasswordHasher(userPasswordDOT.NewPassword);

                    _userService.UpdateUser(validUser);
                    await _userService.SaveChangesAsync();
                    return Ok("Updated Successfully");
                }

                catch (Exception)
                {
                    return StatusCode(500);
                }
            }

            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }
    }
}
