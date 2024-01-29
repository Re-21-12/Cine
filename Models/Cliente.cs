using System;
using System.Collections.Generic;

namespace Cine.Models
{
    public partial class Cliente
    {
        public int Id
        {
            get; set;
        }
        public string Nit
        {
            get; set; 
        } 
        public string Nombre { get; set; } = null!;
        public int Ticket
        {
            get; set;
        }
        public int NumeroPelicula
        {
            get; set;
        }
        public int NumeroSala
        {
            get; set;
        }
        public int NumeroAsiento
        {
            get; set;
        }

        // Método para actualizar dinámicamente el Ticket
        public int ActualizarTicket()
        {
            string ticket = $"{NumeroPelicula}{NumeroSala}{NumeroAsiento}";
            int ticketInt = int.Parse(ticket);
            return ticketInt;
        }


    }
}
