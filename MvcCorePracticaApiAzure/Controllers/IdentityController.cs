using ApiPracticaAzure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCorePracticaApiAzure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcCorePracticaApiAzure.Controllers {
    public class IdentityController : Controller {

        ServiceAPISeries service;

        public IdentityController(ServiceAPISeries service) { this.service = service; }

        public IActionResult login () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (String userName, String password) {
            String token = await this.service.GetToken(userName, password);
            if (token == null) {
                ViewData["MENSAJE"] = "Usuario/Contreseña incorrectos";
                return View();
            } else {
                UsuariosAzure usuario = await this.service.GetUsuario(token);
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Nombre.ToString()));

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.Now.AddMinutes(15)
                        });

                //para poder trabajar necesitamos almacenar el token para las peticiones.
                HttpContext.Session.SetString("TOKEN", token);
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout () {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (HttpContext.Session.GetString("TOKEN") != null) {
                HttpContext.Session.Remove("TOKEN");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
