using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor
        // Os testes devem Falhar
        // Os testes devem passar
        // Refatorar código

        [TestMethod]
        public void DeveRetornarErroQuandoCNPJForInvalido()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void DeveRetornarSucessoQuandoCNPJForValido()
        {
            var doc = new Document("99097688000137", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void DeveRetornarErroQuandoCPFForInvalido()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataRow("42712797060")]
        [DataRow("42712797060")]
        [DataRow("42712797060")]
        public void DeveRetornarSucessoQuandoCPFForValido(string value)
        {
            var doc = new Document(value, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }

    }
}