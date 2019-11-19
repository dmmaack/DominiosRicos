using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void DevoRetornarErroQuandoNomeEstaInvalido()
        {
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = string.Empty;

            command.Validate();
            Assert.IsTrue(command.Invalid);
        }
    }
}