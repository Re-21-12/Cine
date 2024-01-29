using System;
using System.Collections.Generic;

namespace Cine.Models
{
    public partial class Asiento
    {
        public string? Estado { get; set; }
        public int? NumeroSala { get; set; }
        public int NumeroAsiento { get; set; }

        public virtual Sala? NumeroSalaNavigation { get; set; }
        //Creando un constructor
        public Asiento(string Estado, int NumeroSala,  int NumeroAsiento)
        {
            this.Estado = Estado;
            this.NumeroSala = NumeroSala;
            this.NumeroAsiento = NumeroAsiento;
        }
        public Asiento()
        {
        }
    }
}
