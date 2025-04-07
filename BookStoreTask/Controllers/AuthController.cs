using BookStoreTask.DTOs;
using BookStoreTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromBody] RegisterDto registerDto)
        {
            _logger.LogInformation("Register request received for email: {Email}", registerDto.Email);

            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                _logger.LogWarning("Register failed: {Error}", result.Error);
                return BadRequest(result);
            }

            _logger.LogInformation("Register successful for email: {Email}", registerDto.Email);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            _logger.LogInformation("Register request received for email: {Email}", registerDto.Email);

            var result = await _authService.RegisterAsync(registerDto);

            if (!result.Success)
            {
                _logger.LogWarning("Register failed: {Error}", result.Error);
                return BadRequest(result);
            }

            _logger.LogInformation("Register successful for email: {Email}", registerDto.Email);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Login request received for email: {Email}", loginDto.Email);

            var result = await _authService.LoginAsync(loginDto);

            if (!result.Success)
            {
                _logger.LogWarning("Login failed: {Error}", result.Error);
                return BadRequest(result);
            }

            _logger.LogInformation("Login successful for email: {Email}", loginDto.Email);
            return Ok(result);
        }
    }
}