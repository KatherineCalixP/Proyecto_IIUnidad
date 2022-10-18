using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        //propiedades en los campos de las tablas 
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string? Correo { get; set; }
        public string Rol { get; set; }
        public bool EstActivo { get; set; }
    }
}
