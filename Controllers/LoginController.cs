using Cine.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers
{
    public class LoginController : Controller
    {
        //sobrecarga de acciones
        public IActionResult Validar()
        {
        return View();
        }

        [HttpPost]
        public IActionResult Validar(Login login)
        {
            if (login.Usuario == "Admin" && login.Password == "AdministradorCine")
            {
                // Usuario autenticado con éxito
                TempData["Mensaje"] = "Inicio de sesión exitoso.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Error de autenticación
                TempData["Mensaje"] = "Error de inicio de sesión. Usuario o contraseña incorrectos.";
                return View("Error");
            }
        }

        public IActionResult Error()
        {
            // Acción para manejar errores de inicio de sesión
            return View();
        }
    }
}
