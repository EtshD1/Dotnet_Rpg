using Dotnet_Rpg.Dtos.Auth;
using Dotnet_Rpg.Models;
using Dotnet_Rpg.Services.AuthToken;
using Dotnet_Rpg.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Rpg.Controllers
{
    [ApiController]
    [Route("api/characters")]
    public class AuthController : ControllerBase
	{
        private readonly IAuthTokenService _authTokenService;
        private readonly IUserService _userService;

        public AuthController(IAuthTokenService authTokenService, IUserService userService)
		{
            _authTokenService = authTokenService;
            _userService = userService;
        }

		[HttpPost("register")]
		public async Task<ActionResult<ResponseService<AccessTokenDto>>> Register(AuthDto req){
			var userRes = await _userService.Register(req);

			if (!userRes.Success)
			    return BadRequest(userRes);

			var authRes = await _authTokenService.GetTokens(userRes.Data!);

			var response = new ResponseService<AccessTokenDto> {
				Data = new AccessTokenDto {
					AccessToken = authRes.accessToken
				}
			};

			response.Message = userRes.Message;

			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = authRes.refreshToken.Expires
			};
			Response.Cookies.Append("refreshToken", authRes.refreshToken.Token, cookieOptions);

			return response;
		}


		[HttpPost("login")]
		public async Task<ActionResult<ResponseService<AccessTokenDto>>> login(AuthDto req){
			var userRes = await _userService.Login(req);

			if (!userRes.Success)
			    return BadRequest(userRes);

			var authRes = await _authTokenService.GetTokens(userRes.Data!);

			var response = new ResponseService<AccessTokenDto> {
				Data = new AccessTokenDto {
					AccessToken = authRes.accessToken
				}
			};

			response.Message = userRes.Message;

			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = authRes.refreshToken.Expires
			};
			Response.Cookies.Append("refreshToken", authRes.refreshToken.Token, cookieOptions);

			return response;
		}
	}
}
