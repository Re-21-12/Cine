using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cine.Models
{
    public partial class Cliente
    {
        //corregion al error de identity set off
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
