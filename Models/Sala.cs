using System;
using System.Collections.Generic;

namespace Cine.Models
{
    public partial class Sala
    {
        public Sala()
        {
            AsientosNavigation = new HashSet<Asiento>();
            Peliculas = new HashSet<Pelicula>();
        }

        public int Asientos { get; set; }
        public string? Estado { get; set; }
        public int NumeroSala { get; set; }

        public virtual ICollection<Asiento> AsientosNavigation { get; set; }
        public virtual ICollection<Pelicula> Peliculas { get; set; }
    }
}
