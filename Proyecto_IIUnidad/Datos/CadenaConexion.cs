using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public static class CadenaConexion
    {
        //Instanciar objeto 
        //clase estatica es para que esa clase no sea nwcesario instanciar un objeto se accede directamente 

        //Propiedad para conectarnos a la base de datos
        public static string Cadena = "Data Source=Localhost; Initial Catalo=MiFactura; User ID=root; Password=programacion3";
    }
}
