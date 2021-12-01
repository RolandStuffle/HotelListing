using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data.Entities;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserTO loginUserTo)
        {
            _logger.LogInformation($"Login attempt for {loginUserTo.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(loginUserTo))
                {
                    return Unauthorized();
                }

                return Accepted(new {Token = await _authManager.CreateToken()});
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(Login)}", ex);
                return Problem($"Something went wrong in {nameof(Login)}", statusCode: 500);
            }
        }
    }
}