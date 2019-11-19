using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool DocumentExists(string document)
        {
            if (document.Equals("42712797060"))
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email.Equals("emailteste@email.com"))
                return true;

            return false;
        }
    }
}