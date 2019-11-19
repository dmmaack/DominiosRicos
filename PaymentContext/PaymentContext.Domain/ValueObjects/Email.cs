using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string adress)
        {
            this.Adress = adress;

            AddNotifications(new Contract()
            .Requires()
            .IsEmail(Adress, "Email.Adress", "E-mail inválido"));

        }
        public string Adress { get; private set; }
    }
}