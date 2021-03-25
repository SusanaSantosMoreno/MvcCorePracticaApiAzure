using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcCore.Models;
using MvcCorePracticaApiAzure.Filters;
using MvcCorePracticaApiAzure.Services;

namespace MvcCore.Controllers{
    public class HomeController : Controller {

        public ServiceAPISeries service;

        public HomeController (ServiceAPISeries service) { this.service = service; }

        public async Task<IActionResult> Index(int idserie) {
            List<Personaje> personajes = new List<Personaje>();
            if (idserie == 0) {
                personajes = await this.service.GetPersonajesAsync();
            } else {
                personajes = await this.service.GetPersonajesSerieAsync(idserie);
            }
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["Personajes"] = personajes;
            return View(series);
        }

        [UsuarioAuthorize]
        public async Task<IActionResult> NuevoPersonaje () {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        [HttpPost]
        [UsuarioAuthorize]
        public async Task<IActionResult> NuevoPersonaje(int idPersonaje, string nombre, 
            string imagen, int idSerie) {
            await this.service.InsertarPersonajeAsync(idPersonaje, nombre, imagen, idSerie);
            return RedirectToAction("Index");
        }

        [UsuarioAuthorize]
        public async Task<IActionResult> ModificarPersonaje () {
            List<Serie> series = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewData["Personajes"] = personajes;
            return View(series);
        }

        [HttpPost]
        [UsuarioAuthorize]
        public async Task<IActionResult> ModificarPersonaje (int idPersonaje, int idSerie) {
            await this.service.CambiarPersonajeSerieAsync(idPersonaje, idSerie);
            return RedirectToAction("Index");
        }
    }
}
