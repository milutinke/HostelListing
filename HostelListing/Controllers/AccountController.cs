using AutoMapper;
using HostelListing.Data;
using HostelListing.Models;
using HostelListing.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HostelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager, ILogger<CountryController> logger, IMapper mapper, IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid model state!");
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDto.Roles);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)} controller");
                return Problem($"Something went wrong in the {nameof(Register)} controller", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(userDto))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)} controller");
                return Problem($"Something went wrong in the {nameof(Register)} controller", statusCode: 500);
            }
        }
    }
}
