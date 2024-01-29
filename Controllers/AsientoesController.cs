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
    public class AsientoesController : Controller
    {
        // Crea una instancia que solo se puede leer del contexto
        private readonly CineContext _context;

        //a traves de esta variable inyecta  el contenido de la bd
        public AsientoesController(CineContext context)
        {
            _context = context;
        }
        //<a asp-controller="Asientoes" asp-action="CrearAsientos" asp-route-id="@item.NumeroSala" class="btn btn-success">Generar Asientos</a>
        /*
        [HttpGet]
        public IActionResult CrearAsientos()
        {
            return View();
        }*/
        //esta funcion es la encargada de devolver el numero de asientos
        private int ObtenerAsientoSala(int NumeroSala)
        {
            // Usando el contexto, obtenemos la cantidad de asientos para la sala específica
            var salaEncontrada = _context.Salas.Where(sala => sala.NumeroSala == NumeroSala).ToList();
            foreach (var sala in salaEncontrada)
            {
                return sala.Asientos;
            }
            return 0;
        }
        [HttpPost]
        public async Task<IActionResult> CrearAsientos(int NumeroSala)
        {
            //trae loos asientos de el atributo Asientos con el numero de sala

            //75
            int asientosEnSala= ObtenerAsientoSala(NumeroSala);

            //50
            int totalAsientosTabla = _context.Asientos.Count();

            int cantidadAsientosNueva = asientosEnSala + totalAsientosTabla;
            // Realiza alguna lógica adicional con la cantidad de asientos, por ejemplo, crear más asientos

            for (int i = totalAsientosTabla ; i < cantidadAsientosNueva; i++)
                {
             Asiento asiento = new("Disponible", NumeroSala, i);
                _context.Add(asiento);
                }
            await _context.SaveChangesAsync();
            //ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", asiento.NumeroSala);

            // Redirige a la acción "Index" o la acción que desees después de realizar la operación
            return RedirectToAction("Index", "Asientoes");
        }
        /*
         Asi se veria la consulta
        SELECT *
FROM Sala Where Sala.Asientos = NumeroSala
         */
        // GET: Asientoes
        [HttpGet]
        public async Task<IActionResult> Index(string? filtrarEstado, int? NumeroSala)
        {
            //extrae una lista de asientos
            var Asientos = from Asiento in _context.Asientos select Asiento;

            //si no es nulo o vacio vas a mostrar los asientos que coincidan con el estado

            //si no es nulo y tiene un estado y una sala entonces devuelve el filtro de ambos
            if (!string.IsNullOrEmpty(filtrarEstado) && NumeroSala.HasValue)
            {
                // Filtro basado en Estado y NumeroSala
                Asientos = Asientos.Where(asiento => asiento.Estado == filtrarEstado && asiento.NumeroSala == NumeroSala);
            }
            //si solo tiene estado devuelve estado
            else if (!string.IsNullOrEmpty(filtrarEstado))
            {
                // Filtro basado solo en Estado
                Asientos = Asientos.Where(asiento => asiento.Estado == filtrarEstado);
            }
            //si solo tiene el valor entonces devuelve el valor
            else if (NumeroSala.HasValue)
            {
                // Filtro basado solo en NumeroSala
                Asientos = Asientos.Where(asiento => asiento.NumeroSala == NumeroSala);
            }
        
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala");

            return View(await Asientos.ToListAsync());
        }

        // GET: Asientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.NumeroSalaNavigation)
                .FirstOrDefaultAsync(m => m.NumeroAsiento == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // GET: Asientoes/Create
        public IActionResult Create()
        {
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala");
            return View();
        }

        // POST: Asientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Estado,NumeroSala,NumeroAsiento")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", asiento.NumeroSala);
            return View(asiento);
        }

        // GET: Asientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", asiento.NumeroSala);
            return View(asiento);
        }

        // POST: Asientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Estado,NumeroSala,NumeroAsiento")] Asiento asiento)
        {
            if (id != asiento.NumeroAsiento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoExists(asiento.NumeroAsiento))
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
            ViewData["NumeroSala"] = new SelectList(_context.Salas, "NumeroSala", "NumeroSala", asiento.NumeroSala);
            return View(asiento);
        }

        // GET: Asientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.NumeroSalaNavigation)
                .FirstOrDefaultAsync(m => m.NumeroAsiento == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // POST: Asientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asientos == null)
            {
                return Problem("Entity set 'CineContext.Asientos'  is null.");
            }
            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento != null)
            {
                _context.Asientos.Remove(asiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoExists(int id)
        {
          return (_context.Asientos?.Any(e => e.NumeroAsiento == id)).GetValueOrDefault();
        }
    }
}
