using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCode
{


    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void Suma_CominoFeliz()
        {
            // Arrange (Organizar/Inicializar)
            int a = 4;
            int b = 4;
            int espero = 8;
            // Act (Actuar)
            int actual = UnitTest.Calcular.Suma(a, b);
            // Assert (Confirmar/Coprobar)
            Assert.AreEqual(espero, actual);

        }

        [DataRow(4, 4, 8)]
        [DataRow(5, 5, 10)]
        [DataRow(3, 3, 1)]
        [DataRow(1, 1, 10)]
        [TestMethod]
        public void Suma_ArrayPruebasCominoFeliz(int a, int b, int espero)
        {
            // Arrange (Organizar/Inicializar)

            // Act (Actuar)
            int actual = UnitTest.Calcular.Suma(a, b);
            // Assert (Confirmar/Coprobar)
            Assert.AreEqual(espero, actual);

        }







    }
}
