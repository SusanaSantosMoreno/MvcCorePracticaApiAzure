using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaAzure.Models {
    public class UsuariosAzure {

        public int IdUsuario { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Email { get; set; }
        public String Pass { get; set; }

        public UsuariosAzure () {}

        public UsuariosAzure (int idUsuario, string nombre, 
            string apellidos, string email, string pass) {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellidos = apellidos;
            Email = email;
            Pass = pass;
        }
    }
}
