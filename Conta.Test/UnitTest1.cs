using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Principal;

namespace ContaTest.MSTest
{
    [TestClass]
    public class UnitTest1
    {
        Conta conta;

        [TestInitialize]
        public void SetUp()
        {
           conta = new Conta("0009", 200);
        }

        [Ignore]
        [TestCategory("Sacar Valores")]
        [TestMethod]
        public void PermiteSacarValor()
        {
            bool resultado = conta.Sacar(150);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void NaoPermiteSacarValorSemSaldo()
        {
            bool resultado = conta.Sacar(250);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void NaoPermiteSacarValorNegativo()
        {
            bool resultado = conta.Sacar(-250);

            Assert.IsFalse(resultado);
        }
    }
}
