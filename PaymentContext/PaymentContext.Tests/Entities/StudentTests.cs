
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _adress;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name(firstName: "Bruce", lastName: "Wayne");
            _document = new Document(number: "42712797060", documentType: EDocumentType.CPF);
            _email = new Email(adress: "batman@dc.com");
            _adress = new Address(street: "Rua 1", number: "12345", neighborhood: "Bairro legal", city: "Gotham", state: "SP", country: "Brasil", zipCode: "03218149");
            _student = new Student(name: _name, document: _document, email: _email);

            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void DevoRetornarErroQuandoHouverInscricaoAtiva()
        {
            
             var payment = new PayPalPayment(transactionCode: "1234567", paidDate: DateTime.Now, expireDate: DateTime.Now.AddDays(5), 
                                            total: 10, totalPaid: 10, "NovaEmpresa", document: _document, address: _adress, email: _email );
                                            
            _subscription.AddPayment(payment);
            _student.AddSubscription(subscription: _subscription);
            _student.AddSubscription(subscription: _subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void DevoRetornarErroQuandoNaoHouverPagamento()
        {       
            _student.AddSubscription(subscription: _subscription);
            Assert.IsTrue(_student.Invalid);

        }

        [TestMethod]
        public void DevoRetornarSucessoQuandoAdicionarInscricao()
        {
            var payment = new PayPalPayment(transactionCode: "1234567", paidDate: DateTime.Now.AddDays(-1), expireDate: DateTime.Now.AddDays(5), 
                                            total: 10, totalPaid: 10, "NovaEmpresa", document: _document, address: _adress, email: _email );
                                            
            _subscription.AddPayment(payment);
            _student.AddSubscription(subscription: _subscription);

            Assert.IsTrue(_student.Valid);

        }
    }
}
