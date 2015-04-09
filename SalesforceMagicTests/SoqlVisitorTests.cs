using System;
using System.Linq.Expressions;
using NUnit.Framework;
using SalesforceMagic.LinqProvider;
using Shouldly;

namespace SalesforceMagicTests
{
    public class SoqlVisitorTests
    {
        [Test]
        public void ShouldConvertExpressionsToSoql()
        {
            TestExpression(a => a.IsDeleted).ShouldBe("IsDeleted = True");
            TestExpression(a => !a.IsDeleted).ShouldBe("IsDeleted != True");
        }

        private string TestExpression(Expression<Func<TestAccount, bool>> expression)
        {
            return SOQLVisitor.ConvertToSOQL(expression);
        }

        private class TestAccount
        {
            public bool IsDeleted { get; set; }
        }
    }
}
