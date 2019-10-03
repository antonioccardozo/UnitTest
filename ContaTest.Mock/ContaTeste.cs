using Moq;
using NUnit.Framework;
using Principal;
using System;

namespace ContaTestMock
{
    [TestFixture]
    public class ContaTeste
    {

        [Test]
        public void testSolicitarEmprestimo()
        {

            var conta = new Conta("0015", 100);

            var mock = new Mock<IValidadorCredito>();
            mock.Setup(x => x.ValidarCredito(It.IsAny<string>() , It.Is<decimal>(i => i <= 5000))).Returns(true);
            conta.SetValidadorCredito(mock.Object);
            int resultadoEsperado = 5100;

            conta.SolicitarEmprestimo(5000);

            Assert.IsTrue(conta.GetSaldo() == resultadoEsperado);
        }

        [Test]
        public void testSolicitarEmprestimoComException()
        {
            var conta = new Conta("0015", 100);

            var mock = new Mock<IValidadorCredito>();

            mock.Setup(x => x.ValidarCredito(It.IsAny<string>(), It.IsAny<decimal>())).Throws<InvalidOperationException>();

            conta.SetValidadorCredito(mock.Object);
            int resultadoEsperado = 100;

            conta.SolicitarEmprestimo(5000);

            Assert.IsTrue(conta.GetSaldo() == resultadoEsperado);
        }

        [Test]
        public void testSolicitarEmprestimoAcimaLimite()
        {
            string cpf = "0001";

            Conta conta = new Conta(cpf, 100);
            var mock = new Mock<IValidadorCredito>();
            conta.SetValidadorCredito(mock.Object);

            bool resultado = conta.SolicitarEmprestimo(500);

            mock.Verify(x => x.ValidarCredito(It.IsAny<string>(), It.IsAny<decimal>()), Times.Exactly(2));
        }
    }
}
