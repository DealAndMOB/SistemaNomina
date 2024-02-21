using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using SistemaNomina.Recursos;
using SistemaNomina.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace SistemaNomina.Controllers
{
    public class Inicio : Controller
    {
        private readonly IUsuarioService _UsuarioServicio;

        public Inicio(IUsuarioService usuarioServicio)
        {
            _UsuarioServicio = usuarioServicio;
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuarios model)
        {
            model.Clave = Utilidades.EncriptarClave(model.Clave);

            Usuarios UsuarioCreado = await _UsuarioServicio.SaveUsuarios(model);

            if(UsuarioCreado.Id > 0)
            {
                return RedirectToAction("IniciarSesion", "Inicio");
            }

            ViewData["Mensaje"] = "no se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(String correo, String Clave )
        {
            Usuarios usuarioEncontrado = await _UsuarioServicio.GetUsuarios(correo, Utilidades.EncriptarClave(Clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron Coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
