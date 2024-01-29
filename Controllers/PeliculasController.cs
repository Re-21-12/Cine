using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cine.Models;

namespace Cine.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly CineContext _context;

        public PeliculasController(CineContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(int filtrarEstado)
        {
            var Peliculas = from Pelicula in _context.Peliculas select Pelicula;

            if (filtrarEstado != null)
            {
                Peliculas = Peliculas.Where(pelicula => pelicula.Estado == filtrarEstado);
            }

            return View(await Peliculas.ToListAsync());
        }

        public async Task<IActionResult> UltimasPeliculas(string? nombrePelicula, DateTime? dia, TimeSpan? horaInicio)
        {
            //ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", pelicula.NumeroSala);

            /*
            // Ordena las películas por fecha de creación de manera descendente
            var ultimasPeliculas = await _context.Peliculas
                .OrderByDescending(p => p.Id)
                .Include(p => p.NumeroSalaNavigation)
                .ToListAsync();
                //.Take(3)*/
            var Peliculas = from Pelicula in _context.Peliculas select Pelicula;
            if (!string.IsNullOrEmpty(nombrePelicula))
            {
                Peliculas = _context.Peliculas.Where(Pelicula => Pelicula.Nombre == nombrePelicula && Pelicula.Estado == 1);
            }
            if (dia.HasValue)
            {
                Peliculas = _context.Peliculas.Where(Pelicula => Pelicula.DiaTransmision== dia && Pelicula.Estado == 1);

            }
            if (horaInicio.HasValue)
            {
                Peliculas = _context.Peliculas.Where(Pelicula => Pelicula.Inicio == horaInicio && Pelicula.Estado == 1);
            }
            return View("UltimasPeliculas",await Peliculas.ToListAsync());
        
        }


        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.NumeroSalaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            // Filtra las salas activas o inactivas
            var salasFiltradas = _context.Salas.Where(s => s.Estado == "activo").ToList();

            // Devuelve la vista correspondiente a la acción "Create"
            ViewData["NumeroSala"] = new SelectList(salasFiltradas, "NumeroSala", "NumeroSala");

            return View();
        }


        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Nombre,Clasificacion,DiaTransmision,Inicio,Final,Idioma,Genero,Sinopsis,Actores,Director,Duracion,Estado,NumeroSala,Url")] Pelicula pelicula)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(pelicula);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(pelicula);
            }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", pelicula.NumeroSala);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Clasificacion,DiaTransmision,Inicio,Final,Idioma,Genero,Sinopsis,Actores,Director,Duracion,Estado,NumeroSala,Url")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", pelicula.NumeroSala);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.NumeroSalaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'CineContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
