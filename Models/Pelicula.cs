using System;
using System.Collections.Generic;

namespace Cine.Models
{
    public partial class Pelicula
    {
        public int Id
        {
            get; set;
        }
        public string Nombre { get; set; } = null!;
        public string Clasificacion { get; set; } = null!;
        public DateTime DiaTransmision
        {
            get; set;
        }
        public TimeSpan Inicio
        {
            get; set;
        }
        public TimeSpan Final
        {
            get; set;
        }
        public string Idioma { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string Sinopsis { get; set; } = null!;
        public string Actores { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Duracion
        {
            get
            {
                // Calcular la duración en tiempo real cuando sea necesario
                TimeSpan duracion = Final - Inicio;

                // Formatea la duración en horas y minutos
                string duracionFormateada = $"{(int)duracion.TotalHours} horas {duracion.Minutes} minutos";

                return duracionFormateada;
            }
            set
            {
            }

        }
        public int Estado
        {
            get; set;
        }
        public int? NumeroSala
        {
            get; set;
        }
        public string? Url
        {
            get; set;
        }

        public virtual Sala? NumeroSalaNavigation
        {
            get; set;
        }

      
    }
}
