using Bookstore.Application.Interfaces;
using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GoogleDriveController : ControllerBase
    {
        private readonly IGoogleAuthService _googleAuthService;
        public GoogleDriveController(IGoogleAuthService googleAuthService)
        {
            _googleAuthService = googleAuthService;
        }

        [AllowAnonymous]
        [HttpGet("authorize")]
        public IActionResult AuthorizeGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            };
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            }, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                if (!authenticateResult.Succeeded)
                    return Unauthorized();

                var token = await _googleAuthService.LoginWithGoogleAsync(authenticateResult.Principal);
                return Redirect($"http://localhost:5173/profile?token={Uri.EscapeDataString(token)}");
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "Google authentication failed", error = ex.Message });
            }
        }

        [HttpGet("files")]
        [GoogleScopedAuthorize("https://www.googleapis.com/auth/drive.readonly")]
        public async Task<IActionResult> GetFiles([FromServices] IGoogleAuthProvider auth)
        {
            var credential = await auth.GetCredentialAsync();
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });

            var request = service.Files.List();
            request.PageSize = 5;
            request.Fields = "files(id, name)";
            var files = await request.ExecuteAsync();

            return Ok(files.Files);
        }

        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("GoogleCookie");

            if (!authenticateResult.Succeeded)
                return Unauthorized();

            var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
            return Ok(new { accessToken });
        }
    }
}
