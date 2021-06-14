using System;
using System.Linq;
using System.Linq.Expressions;
using Expressions.Task3.E3SQueryProvider.Models.Entities;
using Xunit;

namespace Expressions.Task3.E3SQueryProvider.Test
{
    public class FtsRequestTranslatorTests
    {
        #region SubTask 1 : operands order

        [Fact]
        public void TestBinaryBackOrder()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => "EPBYMINW0834" == employee.Workstation;

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPBYMINW0834)", translated);
        }

        #endregion

        #region SubTask 2: inclusion operations

        [Fact]
        public void TestBinaryEqualsQueryable()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<IQueryable<EmployeeEntity>, IQueryable<EmployeeEntity>>> expression
                = query => query.Where(e => e.Workstation == "EPBYMINW0834");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPBYMINW0834)", translated);
        }

        [Fact]
        public void TestBinaryEquals()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation == "EPBYMINW0834";

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPBYMINW0834)", translated);
        }

        [Fact]
        public void TestMethodEquals()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.Equals("EPBYMINW0834");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPBYMINW0834)", translated);
        }

        [Fact]
        public void TestStartsWith()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.StartsWith("EPBYMINW0834");
            
            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPBYMINW0834*)", translated);
        }

        [Fact]
        public void TestEndsWith()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.EndsWith("EPBYMINW0834");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(*EPBYMINW0834)", translated);
        }

        [Fact]
        public void TestContains()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.Contains("EPBYMINW0834");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(*EPBYMINW0834*)", translated);
        }

        #endregion
    }
}
