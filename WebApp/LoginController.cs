﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    [Route("api")]
    public class LoginController : Controller
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);
            if (account != null)
            {
                //TODO 1: Generate auth cookie for user 'userName' with external id
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.ExternalId),
                    new Claim(ClaimTypes.Role, account.Role),
                };

                var claimsIdentity = new ClaimsIdentity(claims,
                                                        CookieAuthenticationDefaults.AuthenticationScheme,
                                                        ClaimTypes.Name,
                                                        ClaimTypes.Role);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity));

                return Ok();
            }        
            //TODO 2: return 404 if user not found
            return NotFound();
        }
    }
}