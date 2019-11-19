using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType documentType)
        {
            Number = number;
            DocumentType = documentType;

            AddNotifications(new Contract()
                            .Requires()
                            .IsTrue(Validate(), "Document.Number", "Documento inválido"));
        }

        public string Number { get; private set; }
        public EDocumentType DocumentType { get; private set; }

        private bool Validate()
        {
            if (DocumentType == EDocumentType.CNPJ && Number.Length.Equals(14))
                return true;

            if (DocumentType == EDocumentType.CPF && Number.Length.Equals(11))
                return true;

            return false;

        }
    }
}