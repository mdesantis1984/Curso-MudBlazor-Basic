using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest;

namespace UnitTestCode
{
    [TestClass]
    public class UnitTestColeccion
    {
        [TestMethod]
        public void GetEjemplo_CaminoFeliz()
        {
            // Arrange (Organizar/Inicializa)
            Ejemplo[] ejemplos = new Ejemplo[2] { new Ejemplo() { Nombre = "Marco", Apellido = "De Santis" },
                                                  new Ejemplo() { Nombre = "Marcelo", Apellido = "Reynoso" }};
            // Act (Actuar)
            int indice = 1;
            Ejemplo espero = ejemplos[1];

            // Assert (Confirmar/Comprobar)
            Ejemplo actual = ColeccionEjemplo.GetEjemplo(ejemplos, indice);

            // verifico el tipo
            Assert.IsInstanceOfType(actual, typeof(Ejemplo));
            // verifico que no sea null
            Assert.IsNotNull(actual);
            // verifico que el resultado sea lo esperado
            Assert.AreEqual(espero, actual);

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetEjemplo_Excepcion()
        {
            // Arrange (Organizar/Inicializa)
            Ejemplo[] ejemplos = new Ejemplo[2] { new Ejemplo() { Nombre = "Marco", Apellido = "De Santis" },
                                                  new Ejemplo() { Nombre = "Marcelo", Apellido = "Reynoso" }};
            // Act (Actuar)
            int indice = 20;
            ColeccionEjemplo.GetEjemplo(ejemplos, indice);

        }
    }
}
