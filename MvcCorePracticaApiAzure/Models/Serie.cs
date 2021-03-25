using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models {
    public class Serie {

        public int IdSerie { get; set; }
        public String Nombre { get; set; }
        public String Imagen { get; set; }
        public double Puntuacion { get; set; }
        public int Annio { get; set; }

        public Serie () { }

        public Serie (int idSerie, string nombre, 
            string imagen, float puntuacion, int annio) {
            IdSerie = idSerie;
            Nombre = nombre;
            Imagen = imagen;
            Puntuacion = puntuacion;
            Annio = annio;
        }
    }
}
