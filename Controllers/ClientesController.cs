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
    public class ClientesController : Controller
    {
        private readonly CineContext _context;

        public ClientesController(CineContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ? 
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'CineContext.Clientes'  is null.");
        }
       

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if ( _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        private List<Asiento> obtenerAsientos(int NumeroSala) {
            var asientos = _context.Asientos.Where(asiento => asiento.NumeroSala == NumeroSala && asiento.Estado != "Ocupado").ToList();
            return asientos;
        }

        private List<Sala> obtenerSala()
        {
            var salas= _context.Salas.Where(sala => sala.Estado == "activo").ToList();
            return salas;

        }
        //Primero buscamos la pelicula y obtenemoso el numero de sala
        public IActionResult Create(int NumeroPelicula, int NumeroSala)
        {
            var asientosFiltrados = _context.Asientos.Where(asiento => asiento.Estado == "Disponible" && asiento.NumeroSala == NumeroSala).ToList();

            Cliente cliente = new Cliente()
            {
                NumeroPelicula = NumeroPelicula,
                NumeroSala = NumeroSala
            };
            ViewData["NumeroAsiento"] = new SelectList(asientosFiltrados, "NumeroAsiento", "NumeroAsiento");



            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nit,Nombre,NumeroPelicula,NumeroSala,NumeroAsiento")] Cliente cliente, int NumeroSala, int NumeroPelicula)
        {
            if (ModelState.IsValid)
            {
                cliente.NumeroPelicula = NumeroPelicula;
                cliente.NumeroSala = NumeroSala;
                cliente.Ticket = cliente.ActualizarTicket();
                // Obtener el asiento seleccionado
                var asientoSeleccionado =  _context.Asientos
                    .Where(asiento => asiento.NumeroAsiento == cliente.NumeroAsiento).ToList();
                    asientoSeleccionado[0].Estado = "Ocupado";
                    _context.Update(asientoSeleccionado[0]);
              ;//Con este metodo actualizamos o seteamos el ticket en base a los datos obtenidos
                
                // Guardamos el cliente en la base de datos
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                ///retornamos al index
                return RedirectToAction("UltimasPeliculas", "Peliculas");
            }

            return View(cliente);
        }


        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nit,Nombre,Ticket")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'CineContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
