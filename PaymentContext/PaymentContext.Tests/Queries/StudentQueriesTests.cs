using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using System.Linq;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private List<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            
            for (int i = 0; i < 10; i++)
            {
                _students.Add(
                    new Student(
                        name: new Name(firstName: $"Aluno{i}", lastName: $"Sobrenome_{i}"),
                        document: new Document(number: $"1112223334{i}", EDocumentType.CPF),
                        email: new Email(adress: $"aluno{i}@gmail.com")
                    )
                );
            }
        }

        [TestMethod]
        public void DeveRetornarNullQuandoDocumentoNãoExiste()
        {
            var exp = StudentQueries.GetStudentInfo("11122233351");
            var res = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNull(res);
        }

        [TestMethod]
        public void DeveRetornarEstudanteQuandoDocumentoExiste()
        {
            var exp = StudentQueries.GetStudentInfo("11122233341");
            var res = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.IsNotNull(res);
        }
    }
}