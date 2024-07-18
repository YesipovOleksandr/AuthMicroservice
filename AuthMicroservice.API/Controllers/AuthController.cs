using AuthMicroservice.API.ViewModels;
using AuthMicroservice.Domain.Abstract.Services;
using AuthMicroservice.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService,IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("anonuser")]
        public async Task<IActionResult> AnonUser()
        {
            string token = _authService.AnonUser();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.Authenticate(_mapper.Map<User>(model));
                if (user == null)
                {
                    return Unauthorized();
                }
                AuthViewModel result = null;

                result = _mapper.Map<AuthViewModel>(user);

                return Ok(result);
            }
            else
            {
                return ValidationProblem(ModelState);
            }
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.Registration(_mapper.Map<User>(model));
                if (user == null)
                {
                    return BadRequest();
                }

                AuthViewModel result = null;

                result = _mapper.Map<AuthViewModel>(await _authService.Authenticate(_mapper.Map<User>(user)));

                if (result == null)
                {
                    return Unauthorized();
                }

                return Ok(result);
            }
            else
            {
                return ValidationProblem(ModelState);
            }
        }
    }
}
