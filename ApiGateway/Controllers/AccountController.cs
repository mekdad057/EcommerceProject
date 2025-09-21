using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DataLayer.Models;
using DataLayer.Models.Dtos;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;


        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] UserDto model)
        {
            User user = _mapper.Map<User>(model);
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                IEnumerable<string> errors = result.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "Client");
            if (!roleResult.Succeeded)
            {
                IEnumerable<string> errors = roleResult.Errors.Select(e => e.Description);
                return BadRequest(errors);
            }
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok(model);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginByEmail([FromBody] string email)
        {
            User? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("User Doesn't Exist");
            await _signInManager.SignInAsync(user, isPersistent: true);
            UserDto dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            User? user = await _userManager.GetUserAsync(User);
            UserDto dto = _mapper.Map<UserDto>(user); 
            return Ok(dto);
        }
                
    }
}
