using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class Ejemplo
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }


    public class ColeccionEjemplo
    {

        public static Ejemplo GetEjemplo(Ejemplo[] ejemplos, int indice)
        {
            return ejemplos[indice];
        }

    }
}
