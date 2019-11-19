using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Hendlers;

namespace PaymentContext.Domain.Hendlers
{
    public class SubscriptionHendler : Notifiable, IHendler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;
        public SubscriptionHendler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua Assinatura");
            }

            // verificar se o documento já esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // verificar se email esta cadastrado
            if (_repository.DocumentExists(command.Email))
                AddNotification("Document", "Este Email já está em uso");

            // Checar notificações
            if(Invalid)
            return  new CommandResult(false, "Não foi possível realizar sua assinatura");

            // gerar os VOs
            var name = new Name(firstName: command.FirstName, lastName: command.LastName);
            var document = new Document(number: command.Document, documentType: EDocumentType.CPF);
            var email = new Email(adress: command.Email);
            var address = new Address(street: command.Street, number: command.Number, neighborhood: command.Neighborhood,
                                     city: command.City, state: command.State, country: command.Country, zipCode: command.ZipCode);

            // gerar as entidades
            var student = new Student(name: name, document: document, email: email);

            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new BoletoPayment(barCode: command.BarCode, 
                                            boletoNumber: command.BoletoNumber, paidDate: command.PaidDate, expireDate: command.ExpireDate, 
                                            total: command.Total, totalPaid: command.TotalPaid, payer: command.Payer, document: document, address: address, email: email);

            // Relacionamentos
            subscription.AddPayment(pay: payment);
            student.AddSubscription(subscription: subscription);
            student.AddAddress(address);
            
            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // salvar as informações
            _repository.CreateSubscription(student);

            //enviar email de boas vindas
            _emailService.Send(to: student.Name.ToString(), email: student.Email.Adress, subject: "Bem vindo", body: "Sua assinatura foi criada");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com Sucesso");

        }
    }
}