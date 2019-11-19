using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Hendlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.Hendlers
{
    [TestClass]
    public class SubscriptionHendlerTests
    {
        [TestMethod]
        public void DeveRetornarErroQuandoDocumentoExiste()
        {
            var handler = new SubscriptionHendler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.BarCode = "23123123123";
            command.BoletoNumber = "46546546567567";
            command.FirstName = "Chetald";
            command.LastName = "Dodnas";
            command.Document = "42712797060";
            command.Email = "emailteste@email.com";
            command.Adress = "";
            command.PaymentNumber = "1233333";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(5);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Wayne Corp";
            command.PayerDocument = "98400543000109";
            command.PayerDocumentType = Domain.Enums.EDocumentType.CNPJ;
            command.Street = "avfdsbdfgsafdg";
            command.Number = "13232";
            command.Neighborhood = "adasvfdasfasdf";
            command.City = "asdfsdaf";
            command.State = "asdfadsfasd";
            command.Country = "asdfdasfas";
            command.ZipCode = "22222222";
            command.PayerEmail = "emailteste@email.com";

            handler.Handler(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}