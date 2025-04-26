﻿using Core.CPRMSServiceComponents.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Auth.API.Controllers
{
    public class AuthenController : BaseControllerV1
    {
        [HttpGet("getnameproject")]
        public IActionResult GetName()
        {
            var user = new
            {
                Name = "HungHPV", 
                Age = 18  
            };
            return Ok(user);
        }
        // https://localhost:7107/rms/authserver/3f2504e0-4f89-11d3-9a0c-0305e82c3301/authen/getuser
        [HttpGet("getuser")]
        public IActionResult GetUser()
        {
            Guid id = this.AppId;
            return Ok($"Tenant AppId = {AppId}");
        }
        [HttpGet("googlelogin")]
        public IActionResult GoogleLogin()
        {
            var clientId = "75558424470-h5fg4osu2s6q7kur53fse0dq0tj3m6bc.apps.googleusercontent.com";
            //var redirectUri = "https://localhost:7093/api/user/Auth/google-callback";
            var redirectUri = "https://localhost:7092/Login?handler=GoogleCallback";
            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/auth" +
                                $"?client_id={clientId}" +
                                $"&redirect_uri={redirectUri}" +
                                $"&response_type=code" +
                                $"&scope=openid%20email%20profile" +
                                $"&access_type=offline";

            return Redirect(googleAuthUrl);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback([FromQuery] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Authorization code is missing!");

            // Đổi Authorization Code lấy Access Token từ Google
            //var tokenResponse = await GetGoogleAccessToken(code);

            //if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.access_token))
            //    return BadRequest("Failed to retrieve access token from Google.");

            //// Lấy thông tin user từ Access Token
            //var userInfo = await GetGoogleUserInfo(tokenResponse.access_token);

            // Trả về thông tin user (hoặc tạo JWT token nếu bạn có hệ thống authentication)
            //return Ok(new { user = userInfo, access_token = tokenResponse.access_token });
           // var user = await _userRepository.GoogleLoginAsync(userInfo.Email, userInfo.Name);
            return Ok();

        }
    }
}