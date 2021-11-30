using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserTO userTO)
        {
            _logger.LogInformation($"Registration attempt for {userTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userTO);
                user.UserName = userTO.Email;
                var result = await _userManager.CreateAsync(user, userTO.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, userTO.Roles);
                    return Accepted();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(Register)}", ex);
                return Problem($"Something went wrong in {nameof(Register)}", statusCode: 500);
            }
        }

        // [Route("login")]
        // [HttpPost]
        // public async Task<IActionResult> Login([FromBody] LoginUserTO loginUserTo)
        // {
        //     _logger.LogInformation($"Registration attempt for {loginUserTo.Email}");
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //
        //     try
        //     {
        //         var result = await _signInManager.PasswordSignInAsync(
        //             userName: loginUserTo.Email,
        //             password: loginUserTo.Password,
        //             isPersistent: false,
        //             lockoutOnFailure: false
        //         );
        //
        //         if (!result.Succeeded)
        //         {
        //             return Unauthorized(loginUserTo);
        //         }
        //
        //         return Accepted();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError($"Something went wrong in {nameof(Login)}", ex);
        //         return Problem($"Something went wrong in {nameof(Login)}", statusCode: 500);
        //     }
        // }
    }
}