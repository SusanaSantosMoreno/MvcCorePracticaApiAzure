using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models {
    public class Personaje {

        public int IdPersonaje { get; set; }
        public String Nombre { get; set; }
        public String Imagen { get; set; }
        public int IdSerie { get; set; }

        public Personaje () { }

        public Personaje (int idPersonaje, string nombre, 
            string imagen, int idSerie) {
            IdPersonaje = idPersonaje;
            Nombre = nombre;
            Imagen = imagen;
            IdSerie = idSerie;
        }
    }
}
